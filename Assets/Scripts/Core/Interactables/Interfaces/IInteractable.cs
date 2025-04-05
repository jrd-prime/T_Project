namespace Core.Interactables.Interfaces
{
    public interface IInteractable
    {
        bool CanInteract { get; }
        string InteractionPrompt { get; }
        void Interact();
    }
}
