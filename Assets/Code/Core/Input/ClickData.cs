using UnityEngine;

namespace Code.Core.Input
{
    public struct ClickData
    {
        public MouseButton Button { get; private set; }
        public Vector2 Position { get; private set; }

        public ClickData(MouseButton button, Vector2 position)
        {
            Button = button;
            Position = position;
        }
    }
}
