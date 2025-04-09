using Core.Data;
using UnityEngine;

namespace Game.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 ToVector3(this JVector3 v3) => new(v3.X, v3.Y, v3.Z);
        public static JVector3 ToJVector3(this Vector3 v3) => new(v3.x, v3.y, v3.z);
    }
}
