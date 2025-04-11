using Data.Interactables;
using Game.Gameplay.Character.Player.Impls;
using Infrastructure.Localization;
using ModestTree;

namespace Game.Gameplay.Interactables.Impls
{
    public sealed class CollectableObj : AInteractableObject<CollectableObjData>
    {
        public override string InteractionTipNameId => LocalizationNameID.TipCollect;
        public override void Interact(ICommandExecutor colliderOwner) => Collect();

        private void Collect()
        {
            Log.Warn("// Логика подбора предмета " + gameObject.name);
        }
    }
}
