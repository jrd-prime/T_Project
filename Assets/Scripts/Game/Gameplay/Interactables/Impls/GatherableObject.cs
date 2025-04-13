using System;
using Data.Interactables;
using Game.Anima.Data.Interactables;
using Infrastructure.Localization;
using ModestTree;
using UnityEngine;

namespace Game.Gameplay.Interactables.Impls
{
    public sealed class GatherableObject : AInteractableObject<GatherableObjectData>
    {
        public override string InteractionTipNameId => LocalizationNameID.TipGather;
        protected override void OnInteract() => Gather();

        private void Gather()
        {
            Log.Warn("// Логика сбора растения " + gameObject.name);

            CharacterInteractor.AnimateWithTrigger(InteractableTriggerName.GatherHigh,
                InteractableAnimationName.GatherHigh,
                OnAnimationComplete);

            // var animator = CharacterInteractor.Animator as Animator;
            // if (animator != null) animator.SetTrigger("gather_high");
            // Log.Warn("start wait = " + Time.time);
            // animator.WaitForAnimationCompleteAsync("gath_an", OnAnimationComplete).Forget();
            // Log.Warn("after wait = " + Time.time);
            // var gatherCommand = Container.Resolve<GatherCommand>();
            // colliderOwner.ExecuteCommand(gatherCommand);

            foreach (var @return in data.returns)
            {
                Log.Warn("return: " + Localize(@return.currency.LocalizationKey, WordTransform.Upper) + " / " +
                         @return.min + " - " + @return.max + " pc");
            }
        }

        private void OnAnimationComplete()
        {
            Log.Warn("on animation complete = " + Time.time);

            OnInteractionComplete?.Invoke();
        }
    }
}
