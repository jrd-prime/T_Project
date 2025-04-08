using Core.Interactables.Common;
using ModestTree;

namespace Core.Interactables.Impls
{
    public sealed class OpenableObject : AInteractableObject
    {
        public bool IsOpen { get; private set; }

        public override void Interact() => OpenClose();

        private void OpenClose()
        {
            IsOpen = !IsOpen;
            Log.Warn(IsOpen ? "Opened " : "Closed " + gameObject.name);
        }
    }
}
