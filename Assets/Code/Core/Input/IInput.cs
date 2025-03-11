using R3;
using UnityEngine;

namespace Code.Core.Input
{
    public interface IInput
    {
        public Observable<int> ActionPanelIndex { get; }
        public ReadOnlyReactiveProperty<Vector3> MoveDirection { get; }
        public Observable<GameplayAction> GameplayAction { get; }
        public Observable<MouseClickData> MouseClick { get; }
    }
}
