using Core.Providers.Localization;
using Db.Interactables;
using ModestTree;

namespace Game.Interactables.Impls
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
