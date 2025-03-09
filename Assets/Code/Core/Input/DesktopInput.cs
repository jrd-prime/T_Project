using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using VContainer;

namespace Code.Core.Input
{
    public abstract class JInput : MonoBehaviour
    {
        protected IInputHandlerReceiver InputHandlerReceiver;

        [Inject]
        private void Construct(IInputHandlerReceiver inputHandlerReceiver) =>
            InputHandlerReceiver = inputHandlerReceiver;
    }

    public sealed class DesktopInput : JInput
    {
        private JInputActions _gameInputActions;

        private readonly Dictionary<string, GameplayAction> _actions = new()
        {
            { "/keyboard/escape", GameplayAction.Menu },
            { "/keyboard/i", GameplayAction.Inventory },
            { "/keyboard/e", GameplayAction.Interaction },
        };

        private void Awake()
        {
            _gameInputActions = new JInputActions();
            _gameInputActions.Enable();

            // Move
            _gameInputActions.Hero.Move.performed += OnMoveAction;
            _gameInputActions.Hero.Move.canceled += _ => InputHandlerReceiver.ResetMoveDirection();

            _gameInputActions.GamePlay.SomeActions.performed += OnSomeAction;
            _gameInputActions.GamePlay.SomeActions.canceled +=
                _ => InputHandlerReceiver.SetGameplayAction(GameplayAction.None);

            _gameInputActions.GamePlay.MouseClick.performed += OnMouseClick;
        }

        private void OnMouseClick(InputAction.CallbackContext obj)
        {
            // Определяем, какая кнопка была нажата
            var path = obj.control.path;
            Debug.LogWarning("path: " + path);
            Vector2 clickPosition = Mouse.current.position.ReadValue(); // Позиция клика

            switch (path)
            {
                case "/mouse/LeftClick":
                    Debug.Log($"Left Click at: {clickPosition}");
                    break;
                case "/mouse/RightClick":
                    Debug.Log($"Right Click at: {clickPosition}");
                    break;
                case "/mouse/MiddleClick":
                    Debug.Log($"Middle Click at: {clickPosition}");
                    break;
                default:
                    break;
            }
        }

        private void OnSomeAction(InputAction.CallbackContext obj)
        {
            if (_actions.TryGetValue(obj.control.path, out var action)) InputHandlerReceiver.SetGameplayAction(action);
        }

        private void OnMoveAction(InputAction.CallbackContext context)
        {
            var dir = context.ReadValue<Vector2>();
            InputHandlerReceiver.SetMoveDirection(new Vector3(dir.x, 0, dir.y));
        }
    }

    public enum GameplayAction
    {
        None = 0,
        Menu,
        Interaction,
        Inventory,
    }
}
