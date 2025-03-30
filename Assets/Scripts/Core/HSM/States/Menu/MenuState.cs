using System;
using Core.HSM.Common;
using Core.HSM.Interfaces;
using Core.HSM.States.Gameplay;

namespace Core.HSM.States.Menu
{
    public class MenuState : BaseState
    {
        public MenuState(HSM stateMachine, IUIManager uiManager) : base(stateMachine, uiManager)
        {
        }

        public override void Enter()
        {
            UIManager.ShowView("MenuBackground");
            UIManager.SetBaseView("MenuView");
        }

        public override void Exit()
        {
            UIManager.HideAllViews();
            UIManager.HideView("MenuBackground");
        }

        public override void Update()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.S: UIManager.SwitchToView("SettingsView"); break;
                    case ConsoleKey.A: UIManager.SwitchToView("AudioSettingsView"); break;
                    case ConsoleKey.E: UIManager.SwitchToView("EffectsSettingsView"); break;
                    case ConsoleKey.B: UIManager.ShowPreviousView(); break;
                }
            }
        }

        public override IState HandleTransition()
        {
            if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.G)
                return new GameplayState(StateMachine, UIManager);
            return null;
        }
    }
}
