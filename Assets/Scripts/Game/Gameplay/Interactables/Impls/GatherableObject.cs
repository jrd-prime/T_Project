using Data.Interactables;
using Game.Anima;
using Game.Gameplay.Character.Player.Impls;
using Infrastructure.Localization;
using ModestTree;

namespace Game.Gameplay.Interactables.Impls
{
    public sealed class GatherableObject : AInteractableObject<GatherableObjectData>
    {
        public override string InteractionTipNameId => LocalizationNameID.TipGather;
        public override void Interact(ICommandExecutor colliderOwner) => Gather(colliderOwner);

        private void Gather(ICommandExecutor colliderOwner)
        {
            Log.Warn("// Логика сбора растения " + gameObject.name);

            var gatherCommand = Container.Resolve<GatherCommand>();
            colliderOwner.ExecuteCommand(gatherCommand);
            foreach (var @return in data.returns)
            {
                Log.Warn("return: " + Localize(@return.currency.LocalizationKey, WordTransform.Upper) + " / " +
                         @return.min + " - " + @return.max + " pc");
            }
        }
    }
}
