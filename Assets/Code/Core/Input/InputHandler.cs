using System;
using R3;
using UnityEngine;

namespace Code.Core.Input
{
    public sealed class InputHandler : IInput, IInputHandlerReceiver
    {
        public ReactiveProperty<int> ActionPanelIndex { get; } = new();
        public ReactiveProperty<Vector3> MoveDirection { get; } = new();
        public ReactiveProperty<GameplayAction> GameplayAction { get; } = new();

        public void SetActionPanelIndex(int index) => ActionPanelIndex.Value = index;

        public void SetGameplayAction(GameplayAction action) => GameplayAction.Value = action;

        public void SetMoveDirection(Vector3 direction) => MoveDirection.Value = direction;
        public void ResetMoveDirection() => MoveDirection.Value = Vector3.zero;
    }

    public interface IInputHandlerReceiver
    {
        public void SetActionPanelIndex(int index);
        public void SetMoveDirection(Vector3 direction);
        public void SetGameplayAction(GameplayAction action);
        public void ResetMoveDirection();
    }

    public interface IInput
    {
        public ReactiveProperty<int> ActionPanelIndex { get; }
        public ReactiveProperty<Vector3> MoveDirection { get; }
        public ReactiveProperty<GameplayAction> GameplayAction { get; }
    }
}
