using Data.Interactables;
using Game.Gameplay.Character.Player.Impls;
using Infrastructure.Localization;
using ModestTree;

namespace Game.Gameplay.Interactables.Impls
{
    public sealed class OpenableObject : AInteractableObject<OpenableObjectData>
    {
        public override string InteractionTipNameId => LocalizationNameID.TipOpen;
        public bool IsOpen { get; private set; }

        public override void Interact(ICommandExecutor colliderOwner) => OpenClose();

        private void OpenClose()
        {
            IsOpen = !IsOpen;
            Log.Warn(IsOpen ? "Opened " : "Closed " + gameObject.name);
        }
    }
}
