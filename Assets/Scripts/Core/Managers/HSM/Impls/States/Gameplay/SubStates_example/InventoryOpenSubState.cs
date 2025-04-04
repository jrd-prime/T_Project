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

        public override void Enter(IState previousState)
        {
            Console.WriteLine("Entering InventoryOpen SubState");
        }

        public override void Update()
        {
           
        }

        public override IState HandleTransition() => null;
    }
}
