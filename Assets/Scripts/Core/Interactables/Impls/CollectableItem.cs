using Core.Interactables.Common;
using ModestTree;

namespace Core.Interactables.Impls
{
    public sealed class CollectableItem : AInteractableObject
    {
        public override void Interact() => Collect();

        private void Collect()
        {
            Log.Warn("// Логика подбора предмета " + gameObject.name);
        }
    }
}
