using Core.Interactables.Common;
using ModestTree;

namespace Core.Interactables.Impls
{
    public sealed class LootableObject : AInteractableObject
    {
        public override void Interact() => Loot();

        private void Loot()
        {
            Log.Warn("// Логика обыска " + gameObject.name);
        }
    }
}
