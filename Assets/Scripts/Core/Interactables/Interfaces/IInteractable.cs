using System;
using Core.Character.Common.Interfaces;

namespace Core.Interactables.Interfaces
{
    public interface IInteractable
    {
        bool CanInteract { get; }
        string InteractionTipNameId { get; }
        string LocalizationKey { get; }
        void Interact(ICharacter colliderOwner, Action onInteractionComplete);
    }
}
