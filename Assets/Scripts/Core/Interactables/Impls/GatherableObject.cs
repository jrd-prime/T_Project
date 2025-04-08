using Core.Interactables.Common;
using ModestTree;

namespace Core.Interactables.Impls
{
    public sealed class GatherableObject : AInteractableObject
    {
        public override void Interact() => Gather();

        private void Gather()
        {
            Log.Warn("// Логика сбора растения " + gameObject.name);
        }
    }
}
