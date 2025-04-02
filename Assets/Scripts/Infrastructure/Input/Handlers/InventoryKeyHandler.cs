using Core.Managers.UI.Impls;
using Core.Managers.UI.Interfaces;
using Game.UI.Common;
using Game.UI.Data;
using Infrastructure.Input.Signals;
using ModestTree;
using Zenject;

namespace Infrastructure.Input.Handlers
{
    public sealed class InventoryKeyHandler : KeyHandlerBase<InventoryKeySignal>
    {
        public InventoryKeyHandler(SignalBus signalBus, IUIManager uiManager) : base(signalBus, uiManager)
        {
        }

        protected override void InitializeSubscriptions()
        {
            AddSubscription(OnInventorySignal);
        }

        private void OnInventorySignal(InventoryKeySignal obj)
        {
            Log.Info("Inventory key pressed");
            if (!UIManager.IsGameplayMainViewActive()) return;
            UIManager.ShowView(ViewRegistryType.Gameplay, "inventory", UIViewer.Layer.Top, replace: false);
        }
    }
}
