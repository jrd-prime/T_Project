using R3;
using UnityEngine;

namespace Core.Character.Common
{
    public interface IFollowable
    {
        ReactiveProperty<Vector3> Position { get; }
    }
}
