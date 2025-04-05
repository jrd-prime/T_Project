using Core.Managers.HSM.Impls;
using Core.Managers.HSM.Impls.States.Gameplay;
using Core.Managers.HSM.Impls.States.Menu;
using Core.Managers.HSM.Signals;
using Core.Managers.UI.Interfaces;
using Game.UI.Common;
using Game.UI.Data;
using Infrastructure.Input.Signals;
using Infrastructure.Input.Signals.Keys;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Infrastructure.Input.Handlers
{
    public sealed class EscapeKeyHandler : KeyHandlerBase<EscapeKeySignal>
    {
        public EscapeKeyHandler(SignalBus signalBus, IUIManager uiManager, HSM hsm) : base(signalBus, uiManager, hsm)
        {
        }

        protected override void InitializeSubscriptions()
        {
            AddSubscription(OnEscapeSignal);
        }

        private void OnEscapeSignal(EscapeKeySignal signal)
        {
            if (HSM.CurrentState == null) return;

            switch (HSM.CurrentState)
            {
                case MenuState:
                    if (!UIManager.IsMainViewActive(ViewRegistryType.Menu))
                    {
                        Log.Warn("Not main view active");
                        if (UIManager.IsViewActive(ViewIDConst.Pause))
                        {
                            Log.Warn("Pause view active");
                            SignalBus.Fire(new ChangeGameStateSignalVo(typeof(GameplayState)));
                            return;
                        }
                        UIManager.ShowPreviousViewNew();
                    }

                    break;
                case GameplayState:
                    if (UIManager.IsOverlayViewActive())
                    {
                        UIManager.CloseOverlayView();
                        return;

                    }
                    
                    if (UIManager.IsMainViewActive(ViewRegistryType.Gameplay))
                    {
                        SignalBus.Fire(new ChangeGameStateSignalVo(typeof(MenuState)));
                    }

                    break;
                default:
                    Log.Error("unknown state");
                    break;
            }
        }
    }
}
