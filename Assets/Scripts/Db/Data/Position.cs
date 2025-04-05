using UnityEngine;

namespace Db.Data
{
    public record Position
    {
        public readonly float X;
        public readonly float Y;
        public readonly float Z;

        public Position(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static implicit operator Vector3(Position pos) => new Vector3(pos.X, pos.Y, pos.Z);
        public static implicit operator Position(Vector3 vec) => new Position(vec.x, vec.y, vec.z);
    }
}
