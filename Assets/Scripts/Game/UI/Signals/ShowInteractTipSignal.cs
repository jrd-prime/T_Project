using UnityEngine;

namespace Game.UI.Signals
{
    public record ShowInteractTipSignal((string, string) TextTuple, Vector3 WorldPosition)
    {
        public (string, string) TextTuple { get; } = TextTuple;
        public Vector3 WorldPosition { get; } = WorldPosition;
    }
}
