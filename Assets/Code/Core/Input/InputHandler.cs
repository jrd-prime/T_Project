using JetBrains.Annotations;
using R3;
using UnityEngine;

namespace Code.Core.Input
{
    [UsedImplicitly]
    public sealed class InputHandler : IInput, IInputSender
    {
        private readonly Subject<int> _actionPanelIndex = new();
        private readonly Subject<Vector3> _moveDirection = new();
        private readonly Subject<GameplayAction> _gameplayAction = new();
        private readonly Subject<ClickData> _mouseClick = new();

        public Observable<int> ActionPanelIndex => _actionPanelIndex.AsObservable();
        public ReadOnlyReactiveProperty<Vector3> MoveDirection => _moveDirection.ToReadOnlyReactiveProperty();
        public Observable<GameplayAction> GameplayAction => _gameplayAction.AsObservable();
        public Observable<ClickData> MouseClick => _mouseClick.AsObservable();

        public void SendActionPanelIndex(int index) => _actionPanelIndex.OnNext(index);
        public void SendMoveDirection(Vector3 direction) => _moveDirection.OnNext(direction);
        public void SendGameplayAction(GameplayAction action) => _gameplayAction.OnNext(action);
        public void SendMouseClick(ClickData clickData) => _mouseClick.OnNext(clickData);
        public void ResetMoveDirection() => _moveDirection.OnNext(Vector3.zero);
    }

    public interface IInputSender
    {
        public void SendActionPanelIndex(int index);
        public void SendMoveDirection(Vector3 direction);
        public void SendGameplayAction(GameplayAction action);
        public void SendMouseClick(ClickData clickData);
        public void ResetMoveDirection();
    }
}
