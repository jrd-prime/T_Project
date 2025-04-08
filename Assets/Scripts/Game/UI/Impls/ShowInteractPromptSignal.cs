using UnityEngine;

namespace Game.UI.Impls
{
    public record ShowInteractPromptSignal(string Text, Vector3 WorldPosition)
    {
        public string Text { get; } = Text;
        public Vector3 WorldPosition { get; } = WorldPosition;
    }
}
