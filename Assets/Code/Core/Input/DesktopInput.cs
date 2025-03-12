using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Code.Core.Input
{
    public abstract class JInput : MonoBehaviour
    {
        protected IInputSender InputSender;

        [Inject]
        private void Construct(IInputSender inputSender) =>
            InputSender = inputSender;
    }

    public sealed class DesktopInput : JInput
    {
        private InputSystem_Actions _gameInputActions;

        private readonly Dictionary<string, GameplayAction> _actions = new()
        {
            { "/keyboard/escape", GameplayAction.Menu },
            { "/keyboard/i", GameplayAction.Inventory },
            { "/keyboard/e", GameplayAction.Interaction },
        };

        private void Awake()
        {
            _gameInputActions = new InputSystem_Actions();
            _gameInputActions.Enable();

            // _gameInputActions.Hero.Move.performed += OnMoveAction;
            // _gameInputActions.Hero.Move.canceled += _ => InputSender.ResetMoveDirection();
            //
            // _gameInputActions.GamePlay.SomeActions.performed += OnSomeAction;
            // _gameInputActions.GamePlay.SomeActions.canceled +=
            //     _ => InputSender.SendGameplayAction(GameplayAction.None);
            //
            // _gameInputActions.GamePlay.MouseClick.performed += OnMouseClick;
        }

        private void OnMouseClick(InputAction.CallbackContext obj)
        {
            var path = obj.control.path;
            var clickPosition = Mouse.current.position.ReadValue();

            var mouseButton = path switch
            {
                "/Mouse/leftButton" => MouseButton.Left,
                "/Mouse/rightButton" => MouseButton.Right,
                "/Mouse/middleButton" => MouseButton.Middle,
                _ => MouseButton.None
            };

            InputSender.SendMouseClick(new ClickData(mouseButton, clickPosition));
        }

        private void OnSomeAction(InputAction.CallbackContext obj)
        {
            if (_actions.TryGetValue(obj.control.path, out var action)) InputSender.SendGameplayAction(action);
        }

        private void OnMoveAction(InputAction.CallbackContext context)
        {
            var dir = context.ReadValue<Vector2>();
            InputSender.SendMoveDirection(new Vector3(dir.x, 0, dir.y));
        }
    }

    public enum MouseButton
    {
        None = -1,
        Left = 0,
        Right = 1,
        Middle = 2
    }

    public enum GameplayAction
    {
        None = -1,
        Menu,
        Interaction,
        Inventory,
    }
}
