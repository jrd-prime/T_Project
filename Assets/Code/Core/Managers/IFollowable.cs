using R3;
using UnityEngine;

namespace Code.Core.Managers
{
    public interface IFollowable
    {
        public ReactiveProperty<Vector3> Position { get; }
        public ReactiveProperty<Quaternion> Rotation { get; }
    }
}
