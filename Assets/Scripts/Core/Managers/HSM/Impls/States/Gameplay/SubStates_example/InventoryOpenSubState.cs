using System;
using Core.Managers.HSM.Common;
using Core.Managers.HSM.Interfaces;
using Core.Managers.UI.Interfaces;

namespace Core.Managers.HSM.Impls.States.Gameplay.SubStates_example
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
                    case ConsoleKey.I: TransitionToSubState<GameplayMainSubState>(); break;
                    case ConsoleKey.B: UIManager.ShowPreviousView(); break;
                }
            }
        }

        public override IState HandleTransition() => null;
    }
}
