using Core.Providers.Localization;
using Db.Interactables;
using ModestTree;

namespace Game.Interactables.Impls
{
    public sealed class GatherableObject : AInteractableObject<GatherableObjectData>
    {
        public override string InteractionTipNameId => LocalizationNameID.TipGather;
        public override void Interact() => Gather();

        private void Gather()
        {
            Log.Warn("// Логика сбора растения " + gameObject.name);
        }
    }
}
