using UnityEngine;

namespace Infrastructure.Input.Signals
{
    public class MoveDirectionSignal
    {
        public Vector3 Direction { get; private set; }
        public MoveDirectionSignal(Vector3 direction) => Direction = direction;
        public void SetDirection(Vector3 direction) => Direction = direction;
    }
}
