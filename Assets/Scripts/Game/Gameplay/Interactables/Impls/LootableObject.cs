using Data.Interactables;
using Infrastructure.Localization;
using ModestTree;

namespace Game.Gameplay.Interactables.Impls
{
    public sealed class LootableObject : AInteractableObject<LootableObjectData>
    {
        public override string InteractionTipNameId => LocalizationNameID.TipLoot;
        public override void Interact() => Loot();

        private void Loot()
        {
            Log.Warn("// Логика обыска " + gameObject.name);
        }
    }
}
