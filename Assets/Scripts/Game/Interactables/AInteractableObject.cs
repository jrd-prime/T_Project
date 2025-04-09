using System;
using Core.Interactables.Interfaces;
using Db;
using UnityEngine;
using Zenject;

namespace Game.Interactables
{
    [RequireComponent(typeof(Collider))]
    public abstract class AInteractableObject<T> : MonoBehaviour, IInteractable where T : InteractableSettings
    {
        [SerializeField] protected T data;

        public bool CanInteract { get; }
        public abstract string InteractionTipNameId { get; }
        public string LocalizationKey => data.LocalizationKey;

        [Inject] private DiContainer Container;

        private void Awake()
        {
            if (!data) throw new NullReferenceException("Interactable data is null. " + name);
        }

        public abstract void Interact();
    }
}
