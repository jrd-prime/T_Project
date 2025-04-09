using Data.Interactables;
using Infrastructure.Localization;
using ModestTree;

namespace Game.Gameplay.Interactables.Impls
{
    public sealed class CollectableObj : AInteractableObject<CollectableObjData>
    {
        public override string InteractionTipNameId => LocalizationNameID.TipCollect;
        public override void Interact() => Collect();

        private void Collect()
        {
            Log.Warn("// Логика подбора предмета " + gameObject.name);
        }
    }
}
