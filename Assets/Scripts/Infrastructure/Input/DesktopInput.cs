using Infrastructure.Input.Common;
using Infrastructure.Input.Interfaces;
using Infrastructure.Input.Signals;
using Infrastructure.Input.Signals.Keys;
using ModestTree;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Infrastructure.Input
{
    public sealed class DesktopInput : MonoBehaviour, IJInput
    {
        [Inject] private SignalBus _signalBus;

        private InputSystem_Actions _inputActions;
        private MoveDirectionSignal _moveDirectionSignal;

        private void Awake()
        {
            var inventoryKeySignal = new InventoryKeySignal();
            var escapeKeySignal = new EscapeKeySignal();
            var interactKeySignal = new InteractKeySignal();

            _moveDirectionSignal = new MoveDirectionSignal(Vector3.zero);

            _signalBus.Subscribe<EnableInputSignal>(OnEnableInputSignal);
            _signalBus.Subscribe<DisableInputSignal>(OnDisableInputSignal);

            _inputActions = new InputSystem_Actions();
            _inputActions.Player.Move.performed += OnMove;
            _inputActions.Player.Move.canceled += OnMove;

            _inputActions.UI.Escape.performed += ctx => { FireSignal(escapeKeySignal, KeyName.Escape); };
            _inputActions.UI.Inventory.performed += ctx => { FireSignal(inventoryKeySignal, KeyName.Inventory); };
            _inputActions.UI.Interact.performed += ctx => { FireSignal(interactKeySignal, KeyName.Interact); };

            _inputActions.UI.QuestLog.performed += ctx => { Debug.LogWarning("Key pressed QuestLog"); };
        }

        private void FireSignal<TSignal>(TSignal signal, string keyName)
        {
            Log.Info($"<color=green>[Pressed]</color> <b>{keyName}</b>");
            _signalBus.Fire(signal);
        }

        private void OnMove(InputAction.CallbackContext ctx)
        {
            var value = ctx.ReadValue<Vector2>();
            var direction = Vector3.Normalize(new Vector3(value.x, 0, value.y));
            _moveDirectionSignal.SetDirection(direction);
            _signalBus.Fire(_moveDirectionSignal);
        }

        private void OnDisableInputSignal()
        {
            Log.Warn("<color=red>Disable input</color>");
            _inputActions.Disable();
        }

        private void OnEnableInputSignal()
        {
            Log.Warn("<color=green>Enable input</color>");
            _inputActions.Enable();
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<EnableInputSignal>(OnEnableInputSignal);
            _signalBus.Unsubscribe<DisableInputSignal>(OnDisableInputSignal);
        }
    }
}
