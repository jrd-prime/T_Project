using Game.Gameplay.Character.Player.Impls;

namespace Core.Interactables.Interfaces
{
    public interface IInteractable
    {
        bool CanInteract { get; }
        string InteractionTipNameId { get; }
        string LocalizationKey { get; }
        void Interact(ICommandExecutor colliderOwner);
    }
}
