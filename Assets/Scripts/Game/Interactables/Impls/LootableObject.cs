using Core.Providers.Localization;
using Db.Interactables;
using ModestTree;

namespace Game.Interactables.Impls
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
