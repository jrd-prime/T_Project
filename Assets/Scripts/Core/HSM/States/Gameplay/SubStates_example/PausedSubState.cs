using System;
using Core.HSM.Common;
using Core.HSM.Interfaces;
using Core.Managers.NewUIManager.Interfaces;

namespace Core.HSM.States.Gameplay.SubStates_example
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
                TransitionToSubState<PlayingSubState>();
        }

        public override IState HandleTransition() => null;
    }
}
