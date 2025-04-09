using System;

namespace Core.Data
{
    [Serializable]
    public record JVector3(float X, float Y, float Z)
    {
        public float X { get; } = X;
        public float Y { get; } = Y;
        public float Z { get; } = Z;
    }
}
