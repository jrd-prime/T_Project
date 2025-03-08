using R3;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Core.Input
{
    public class DesktopInput : MonoBehaviour, IInput
    {
        public ReactiveProperty<Vector3> MoveDirection { get; } = new();

        private JInputActions _gameInputActions;

        private void Awake()
        {
            _gameInputActions = new JInputActions();
            _gameInputActions.Enable();

            _gameInputActions.Hero.Move.performed += OnMove;
            _gameInputActions.Hero.Move.canceled += _ => MoveDirection.Value = Vector3.zero;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            var dir = context.ReadValue<Vector2>();
            MoveDirection.Value = new Vector3(dir.x, 0, dir.y);
        }
    }
}
