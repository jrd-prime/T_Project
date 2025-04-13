using System;
using Data.Interactables;
using Game.Gameplay.Character.Player.Impls;
using Infrastructure.Localization;
using ModestTree;

namespace Game.Gameplay.Interactables.Impls
{
    public sealed class LootableObject : AInteractableObject<LootableObjectData>
    {
        public override string InteractionTipNameId => LocalizationNameID.TipLoot;
        protected override void OnInteract() => Loot();

        private void Loot()
        {
            Log.Warn("// Логика обыска " + gameObject.name);
        }
    }
}
