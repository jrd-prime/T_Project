using System;
using Code.Core.WORK.JStateMachine;
using Code.Core.WORK.UIManager;

namespace Code.Core.WORK.UIStates.Gameplay.State
{
    public class GameplayStateMain : UISubStateBase
    {
        public GameplayStateMain(IUIManager uiManager, GameStateType baseStateType, Enum defaultSubState) : base(
            uiManager,
            baseStateType, defaultSubState)
        {
        }

        public override void Enter()
        {
            ShowView();
        }

        public override void Exit()
        {
            // Hide only when we change BASE state
            // HideView();
        }
    }
}
