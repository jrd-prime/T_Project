namespace Core.Data
{
    public record JQuaternion(float X, float Y, float Z, float W)
    {
        public float X { get; } = X;
        public float Y { get; } = Y;
        public float Z { get; } = Z;
        public float W { get; } = W;
    }
}
