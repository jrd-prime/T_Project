using System;
using Core.Managers.HSM.Common;
using Core.Managers.HSM.Impls.States.Gameplay;
using Core.Managers.HSM.Interfaces;
using Core.Managers.UI.Interfaces;
using Game.UI.Data;

namespace Core.Managers.HSM.Impls.States.Menu
{
    public sealed class MenuState : BaseState
    {
        public MenuState(HSM stateMachine, IUIManager uiManager) : base(stateMachine, uiManager)
        {
        }

        public override void Enter()
        {
            UIManager.SetAndShowBaseView(ViewRegistryType.Menu);
        }

        public override void Exit()
        {
            UIManager.HideAllViews();
        }

        public override IState HandleTransition()
        {
            if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.G)
                return new GameplayState(StateMachine, UIManager);
            return null;
        }
    }
}
