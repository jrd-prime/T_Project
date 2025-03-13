using R3;
using UnityEngine;

namespace Code.Core.Input
{
    public interface IJInput
    {
        public Observable<Vector3> MoveDirection { get; }
    }
}
