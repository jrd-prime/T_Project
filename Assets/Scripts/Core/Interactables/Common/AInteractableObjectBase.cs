using System;
using Core.Interactables.Interfaces;
using ModestTree;
using UnityEngine;

namespace Core.Interactables.Common
{
    [RequireComponent(typeof(Collider))]
    public abstract class AInteractableObjectBase : MonoBehaviour, IInteractable
    {
        public bool CanInteract { get; }
        public string InteractionPrompt { get; }
        
        private void Awake()
        {
        }

        public void Interact()
        {
            Log.Warn(" Interact with " + gameObject.name);
        }
    }
}
