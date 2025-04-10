using Data.Interactables;
using Infrastructure.Localization;
using ModestTree;

namespace Game.Gameplay.Interactables.Impls
{
    public sealed class GatherableObject : AInteractableObject<GatherableObjectData>
    {
        public override string InteractionTipNameId => LocalizationNameID.TipGather;
        public override void Interact() => Gather();

        private void Gather()
        {
            Log.Warn("// Логика сбора растения " + gameObject.name);
            foreach (var @return in data.returns)
            {
                Log.Warn("return: " + Localize(@return.currency.LocalizationKey, WordTransform.Upper) + " / " +
                         @return.min + " - " + @return.max + " pc");
            }
        }
    }
}
