using System;
using Core.HSM.Common;
using Core.HSM.Interfaces;
using Core.Managers.UI.Interfaces;

namespace Core.HSM.States.Gameplay.SubStates_example
{
    public class PlayingSubState : BaseState
    {
        public PlayingSubState(GameplayState parent, IUIManager uiManager) : base(parent.StateMachine, uiManager)
        {
        }

        public override void Enter()
        {
            Console.WriteLine("Entering Playing SubState");
            UIManager.SwitchToView("MainGameplayView");
        }

        public override void Update()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.P: TransitionToSubState<PausedSubState>(); break;
                    case ConsoleKey.I: TransitionToSubState<InventoryOpenSubState>(); break;
                }
            }
        }

        public override IState HandleTransition() => null;
    }
}
