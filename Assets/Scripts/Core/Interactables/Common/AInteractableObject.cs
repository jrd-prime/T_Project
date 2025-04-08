using Core.Interactables.Interfaces;
using UnityEngine;

namespace Core.Interactables.Common
{
    [RequireComponent(typeof(Collider))]
    public abstract class AInteractableObject : MonoBehaviour, IInteractable
    {
        public bool CanInteract { get; }
        public string InteractionPrompt { get; }

        public abstract void Interact();
    }
}
