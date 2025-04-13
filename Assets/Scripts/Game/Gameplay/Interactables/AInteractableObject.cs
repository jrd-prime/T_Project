using System;
using Core.Character.Common.Interfaces;
using Core.Interactables.Interfaces;
using Cysharp.Threading.Tasks;
using Data;
using Infrastructure.Localization;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Interactables
{
    [RequireComponent(typeof(Collider))]
    public abstract class AInteractableObject<T> : MonoBehaviour, IInteractable where T : InteractableSettings
    {
        [SerializeField] protected T data;

        public string LocalizationKey => data.LocalizationKey;
        public bool CanInteract { get; }
        public abstract string InteractionTipNameId { get; }

        [Inject] protected DiContainer Container;
        protected ICharacter Character { get; private set; }
        protected ICharacterInteractor CharacterInteractor { get; private set; }

        private ILocalizationProvider _localizationProvider;

        private void Awake()
        {
            if (!data) throw new NullReferenceException("Interactable data is null. " + name);
            _localizationProvider = Container.Resolve<ILocalizationProvider>();
        }

        protected string Localize(string key, WordTransform wordTransform = WordTransform.None) =>
            _localizationProvider.Localize(key, wordTransform);

        public async UniTask InteractAsync(ICharacter character)
        {
            Character = character;
            CharacterInteractor = character.GetInteractor();

            // Начало взаимодействия
            OnStartInteract();

            // Запуск анимации с тайм-аутом
            bool animationCompleted = await Animate();

            // Обработка завершения анимации
            if (animationCompleted)
            {
                OnAnimationComplete();
            }
            else
            {
                Debug.LogError("Animation timeout!");
            }

            // Финализация взаимодействия
            OnInteractionComplete(animationCompleted);
        }

        protected abstract void OnStartInteract();
        protected abstract UniTask<bool> Animate();
        protected abstract void OnAnimationComplete();
        protected abstract void OnInteractionComplete(bool success);

// Общий метод для запуска анимации с тайм-аутом
        protected async UniTask<bool> PlayAnimationAsync(string triggerName, string animationStateName,
            float timeoutSeconds = 5f)
        {
            var completion = new UniTaskCompletionSource();
            CharacterInteractor.AnimateWithTrigger(
                triggerName,
                animationStateName,
                () => completion.TrySetResult()
            );

            int completedIndex = await UniTask.WhenAny(
                completion.Task,
                UniTask.Delay(TimeSpan.FromSeconds(timeoutSeconds))
            );

            if (completedIndex != 0)
            {
                completion.TrySetResult();
                return false;
            }

            return true;
        }
    }
}
