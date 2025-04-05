using Core.Interactables.Common;
using ModestTree;

namespace Core.Interactables.Impls
{
    public sealed class GarbageBox : AInteractableObjBase
    {
        public bool CanInteract { get; }
        public string InteractionPrompt { get; }

        public void Interact()
        {
            Log.Warn("Interacted with " + nameof(GarbageBox));
        }
    }
}
