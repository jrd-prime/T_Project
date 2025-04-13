using System;
using Core.Character.Common.Interfaces;
using Core.Interactables.Interfaces;
using Data;
using Game.Gameplay.Character.Player.Impls;
using Infrastructure.Localization;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Interactables
{
    [RequireComponent(typeof(Collider))]
    public abstract class AInteractableObject<T> : MonoBehaviour, IInteractable where T : InteractableSettings
    {
        [SerializeField] protected T data;

        public bool CanInteract { get; }
        public abstract string InteractionTipNameId { get; }
        public string LocalizationKey => data.LocalizationKey;

        [Inject] protected DiContainer Container;
        protected ICharacter ColliderOwner { get; private set; }
        protected ICharacterInteractor CharacterInteractor { get; private set; }

        private ILocalizationProvider _localizationProvider;
        protected Action OnInteractionComplete { get; private set; }


        private void Awake()
        {
            if (!data) throw new NullReferenceException("Interactable data is null. " + name);
            _localizationProvider = Container.Resolve<ILocalizationProvider>();
        }

        protected string Localize(string key, WordTransform wordTransform = WordTransform.None) =>
            _localizationProvider.Localize(key, wordTransform);

        public void Interact(ICharacter colliderOwner, Action onInteractionComplete)
        {
            ColliderOwner = colliderOwner;
            OnInteractionComplete = onInteractionComplete;
            CharacterInteractor = colliderOwner.GetInteractor();
            OnInteract();
        }

        protected abstract void OnInteract();
    }
}
