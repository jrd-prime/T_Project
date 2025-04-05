using System;
using Core.Interactables.Interfaces;
using UnityEngine;

namespace Core.Interactables.Common
{
    [RequireComponent(typeof(Collider))]
    public abstract class AInteractableObjBase : MonoBehaviour, IInteractable
    {
        public bool CanInteract { get; }
        public string InteractionPrompt { get; }

        [SerializeField] private InteractableArea _interactableArea;

        private void Awake()
        {
            if (!_interactableArea) throw new NullReferenceException("InteractableArea is null. " + name);
        }

        public void Interact()
        {
            throw new System.NotImplementedException();
        }
    }
}
