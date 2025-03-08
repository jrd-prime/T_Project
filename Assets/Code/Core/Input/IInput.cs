using R3;
using UnityEngine;

namespace Code.Core.Input
{
    public interface IInput
    {
        public ReactiveProperty<Vector3> MoveDirection { get; }
    }
}
