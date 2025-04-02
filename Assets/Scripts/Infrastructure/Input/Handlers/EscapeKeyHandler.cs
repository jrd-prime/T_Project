using System;
using Core.Managers.HSM.Impls.States.Menu;
using Core.Managers.HSM.Signals;
using Core.Managers.UI.Interfaces;
using Core.Managers.UI.Signals;
using Game.UI.Data;
using Infrastructure.Input.Signals;
using UnityEngine;
using Zenject;

namespace Infrastructure.Input.Handlers
{
    public sealed class EscapeKeyHandler : KeyHandlerBase<EscapeKeySignal>
    {
        public EscapeKeyHandler(SignalBus signalBus, IUIManager uiManager) : base(signalBus, uiManager)
        {
        }

        protected override void InitializeSubscriptions()
        {
            AddSubscription(OnEscapeSignal);
        }

        private void OnEscapeSignal(EscapeKeySignal signal)
        {
            Debug.LogWarning("escape key pressed");
            if (UIManager.IsGameplayMainViewActive())
            {
                SignalBus.Fire(new ChangeGameStateSignalVo(typeof(MenuState)));
                SignalBus.Fire(new SwitchLocalViewSignalVo(ViewRegistryType.Menu, "pause"));
            }
            else UIManager.ShowPreviousView();
        }
    }
}
