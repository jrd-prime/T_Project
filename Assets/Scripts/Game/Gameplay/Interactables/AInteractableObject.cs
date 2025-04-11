using System;
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

        private ILocalizationProvider _localizationProvider;


        private void Awake()
        {
            if (!data) throw new NullReferenceException("Interactable data is null. " + name);
            _localizationProvider = Container.Resolve<ILocalizationProvider>();
        }

        protected string Localize(string key, WordTransform wordTransform = WordTransform.None) =>
            _localizationProvider.Localize(key, wordTransform);

        public abstract void Interact(ICommandExecutor colliderOwner);
    }
}
