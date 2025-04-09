using Core.Managers.HSM.Impls;
using Core.Managers.HSM.Impls.States.Gameplay;
using Core.Managers.UI.Data;
using Core.Managers.UI.Interfaces;
using Infrastructure.Input.Signals.Keys;
using Zenject;

namespace Infrastructure.Input.Handlers
{
    public sealed class InventoryKeyHandler : KeyHandlerBase<InventoryKeySignal>
    {
        public InventoryKeyHandler(SignalBus signalBus, IUIManager uiManager, HSM hsm) : base(signalBus, uiManager, hsm)
        {
        }

        protected override void InitializeSubscriptions() => AddSubscription(OnInventorySignal);

        private void OnInventorySignal(InventoryKeySignal obj)
        {
            if (ShouldCloseInventory())
            {
                UIManager.CloseOverlayView();
                return;
            }

            if (UIManager.IsMainViewActive(ViewRegistryType.Gameplay)) ShowInventoryOverlay();
        }

        private bool ShouldCloseInventory() =>
            UIManager.IsOverlayViewActive() &&
            UIManager.IsOverlayIt(ViewIDConst.Inventory) &&
            HSM.CurrentState is GameplayState;

        private void ShowInventoryOverlay()
        {
            var data = new UIManagerViewDataVo(
                ViewRegistryType.Gameplay,
                ViewIDConst.Inventory,
                ViewerLayer.Top,
                true);

            UIManager.ShowView(data);
        }
    }
}
