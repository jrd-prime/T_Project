using System.Linq;
using Core.Managers.HSM.Impls;
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
        public InventoryKeyHandler(SignalBus signalBus, IUIManager uiManager, HSM hsm) : base(signalBus, uiManager, hsm)
        {
        }

        protected override void InitializeSubscriptions()
        {
            AddSubscription(OnInventorySignal);
        }

        private void OnInventorySignal(InventoryKeySignal obj)
        {
            Log.Info("Inventory key pressed");
            if (UIManager.IsMainViewActive(ViewRegistryType.Gameplay))
            {
                Log.Warn("main in gameplay");
                UIManager.ShowViewNew(ViewRegistryType.Gameplay, ViewIDConst.Inventory, UIViewer.Layer.Top, true);
            }
        }
    }
}
