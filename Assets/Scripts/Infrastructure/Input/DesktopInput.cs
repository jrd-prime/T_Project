using System;
using Infrastructure.Input.Handlers;
using Infrastructure.Input.Interfaces;
using Infrastructure.Input.Signals;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Infrastructure.Input
{
    public sealed class DesktopInput : MonoBehaviour, IJInput
    {
        public Observable<Vector3> MoveDirection => _moveDirection.AsObservable();
        public Observable<Unit> OnEscape => _onEscape.AsObservable();
        public Observable<Unit> OnInventory => _onInventory.AsObservable();
        public Observable<Unit> OnQuestLog => _onQuestLog.AsObservable();

        private readonly Subject<Vector3> _moveDirection = new();
        private readonly Subject<Unit> _onEscape = new();
        private readonly Subject<Unit> _onInventory = new();
        private readonly Subject<Unit> _onQuestLog = new();

        private InputSystem_Actions _inputActions;
        [Inject] private SignalBus _signalBus;


        private void Awake()
        {
            _inputActions = new InputSystem_Actions();
            _inputActions.Player.Move.performed += OnMove;
            _inputActions.Player.Move.canceled += OnMove;
            _inputActions.UI.Escape.performed += ctx => { _signalBus.Fire(new EscapeKeySignal()); };
            _inputActions.UI.Inventory.performed += ctx => { _signalBus.Fire(new InventoryKeySignal()); };
            _inputActions.UI.QuestLog.performed += ctx =>
            {
                Debug.LogWarning("Key pressed QuestLog");
                _onQuestLog.OnNext(Unit.Default);
            };
        }

        private void OnMove(InputAction.CallbackContext ctx)
        {
            var value = ctx.ReadValue<Vector2>();
            _moveDirection.OnNext(new Vector3(value.x, 0, value.y));
        }

        private void OnEnable() => _inputActions.Enable();
        private void OnDisable() => _inputActions.Disable();
    }
}
