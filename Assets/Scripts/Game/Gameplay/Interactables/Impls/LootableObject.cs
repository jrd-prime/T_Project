using Cysharp.Threading.Tasks;
using Data.Interactables;
using Infrastructure.Localization;
using ModestTree;

namespace Game.Gameplay.Interactables.Impls
{
    public sealed class LootableObject : AInteractableObject<LootableObjectData>
    {
        public override string InteractionTipNameId => LocalizationNameID.TipLoot;
        protected override void OnStartInteract()
        {
            
        }

        protected override UniTask<bool> Animate()
        {
            return UniTask.FromResult(true);
        }

        protected override void OnAnimationComplete()
        {
        }

        protected override void OnInteractionComplete(bool success)
        {
        }
    }
}
