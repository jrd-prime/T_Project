using Data.Interactables;
using Infrastructure.Localization;
using ModestTree;

namespace Game.Gameplay.Interactables.Impls
{
    public sealed class OpenableObject : AInteractableObject<OpenableObjectData>
    {
        public override string InteractionTipNameId => LocalizationNameID.TipOpen;
        public bool IsOpen { get; private set; }

        public override void Interact() => OpenClose();

        private void OpenClose()
        {
            IsOpen = !IsOpen;
            Log.Warn(IsOpen ? "Opened " : "Closed " + gameObject.name);
        }
    }
}
