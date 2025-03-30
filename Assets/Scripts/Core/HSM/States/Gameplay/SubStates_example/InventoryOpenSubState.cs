using System;
using Core.HSM.Common;
using Core.HSM.Interfaces;
using Core.Managers.NewUIManager.Interfaces;

namespace Core.HSM.States.Gameplay.SubStates_example
{
    public class InventoryOpenSubState : BaseState
    {
        public InventoryOpenSubState(GameplayState parent, IUIManager uiManager) : base(parent.StateMachine, uiManager)
        {
        }

        public override void Enter()
        {
            Console.WriteLine("Entering InventoryOpen SubState");
            UIManager.SwitchToView("InventoryView");
        }

        public override void Update()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.I: TransitionToSubState<PlayingSubState>(); break;
                    case ConsoleKey.B: UIManager.ShowPreviousView(); break;
                }
            }
        }

        public override IState HandleTransition() => null;
    }
}
