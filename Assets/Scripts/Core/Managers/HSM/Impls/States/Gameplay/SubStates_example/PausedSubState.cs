using System;
using Core.Managers.HSM.Common;
using Core.Managers.HSM.Interfaces;
using Core.Managers.UI.Interfaces;

namespace Core.Managers.HSM.Impls.States.Gameplay.SubStates_example
{
    public class PausedSubState : BaseState
    {
        public PausedSubState(GameplayState parent, IUIManager uiManager) : base(parent.StateMachine, uiManager)
        {
        }

        public override void Enter()
        {
            Console.WriteLine("Entering Paused SubState");
            UIManager.SwitchToView("PauseView");
        }

        public override void Update()
        {
            if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.P)
                TransitionToSubState<GameplayMainSubState>();
        }

        public override IState HandleTransition() => null;
    }
}
