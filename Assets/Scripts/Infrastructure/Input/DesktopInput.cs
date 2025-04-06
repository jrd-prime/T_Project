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
            _moveDirectionSignal = new MoveDirectionSignal(Vector3.zero);

            _inputActions = new InputSystem_Actions();
            _inputActions.Player.Move.performed += OnMove;
            _inputActions.Player.Move.canceled += OnMove;
            _inputActions.UI.Escape.performed += ctx =>
            {
                InputLog("ESC");
                _signalBus.Fire(new EscapeKeySignal());
            };
            _inputActions.UI.Inventory.performed += ctx =>
            {
                InputLog("I");
                _signalBus.Fire(new InventoryKeySignal());
            };
            _inputActions.UI.QuestLog.performed += ctx => { Debug.LogWarning("Key pressed QuestLog"); };
        }

        private void OnMove(InputAction.CallbackContext ctx)
        {
            var value = ctx.ReadValue<Vector2>();
            var direction = Vector3.Normalize(new Vector3(value.x, 0, value.y));
            _moveDirectionSignal.SetDirection(direction);
            _signalBus.Fire(_moveDirectionSignal);
        }

        private static void InputLog(string keyName) => Log.Info($"<color=green>[Pressed]</color> <b>{keyName}</b>");

        private void OnEnable() => _inputActions.Enable();
        private void OnDisable() => _inputActions.Disable();
    }
}
