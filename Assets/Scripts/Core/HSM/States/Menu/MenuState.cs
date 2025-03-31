using System;
using Core.HSM.Common;
using Core.HSM.Interfaces;
using Core.HSM.States.Gameplay;
using Core.Managers.UI.Interfaces;

namespace Core.HSM.States.Menu
{
    public sealed class MenuState : BaseState
    {
        public MenuState(HSM stateMachine, IUIManager uiManager) : base(stateMachine, uiManager)
        {
        }

        public override void Enter()
        {
            UIManager.SetBaseView("menu");
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
