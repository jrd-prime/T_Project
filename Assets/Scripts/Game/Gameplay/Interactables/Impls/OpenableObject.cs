using Cysharp.Threading.Tasks;
using Data.Interactables;
using Game.Anima.Data.Interactables;
using Infrastructure.Localization;

namespace Game.Gameplay.Interactables.Impls
{
    public sealed class OpenableObject : AInteractableObject<OpenableObjectData>
    {
        public override string InteractionTipNameId => LocalizationNameID.TipOpen;
        public bool IsOpen { get; private set; }


        protected override void OnStartInteract()
        {
        }

        protected override async UniTask<bool> Animate()
        {
            return await PlayAnimationByTriggerAsync(
                InteractableTriggerName.GatherHigh,
                InteractableAnimationName.GatherHigh
            );
        }

        protected override void OnAnimationComplete()
        {
        }

        protected override void OnInteractionComplete(bool success)
        {
            IsOpen = !IsOpen;
        }
    }
}
