using Cysharp.Threading.Tasks;
using Data.Interactables;
using Game.Anima.Data.Interactables;
using Infrastructure.Localization;
using UnityEngine;

namespace Game.Gameplay.Interactables.Impls
{
    public sealed class GatherableObject : AInteractableObject<GatherableObjectData>
    {
        public override string InteractionTipNameId => LocalizationNameID.TipGather;

        protected override void OnStartInteract()
        {
            Debug.Log($"// Начало сбора растения {gameObject.name}");
        }

        protected async override UniTask<bool> Animate()
        {
            return await PlayAnimationAsync(
                InteractableTriggerName.GatherHigh,
                InteractableAnimationName.GatherHigh
            );
        }

        protected override void OnAnimationComplete()
        {
            foreach (var @return in data.returns)
            {
                Debug.Log($"return: {Localize(@return.currency.LocalizationKey, WordTransform.Upper)} / " +
                          $"{@return.min} - {@return.max} pc");
            }
        }

        protected override void OnInteractionComplete(bool success)
        {
            Debug.Log($"// Завершение сбора растения {gameObject.name}, успех: {success}");
        }
    }
}
