using R3;
using UnityEngine;

namespace Code.Core.Managers
{
    public interface IFollowable
    {
        public ReadOnlyReactiveProperty<Vector3> Position { get; }
        public ReadOnlyReactiveProperty<Quaternion> Rotation { get; }
    }
}
