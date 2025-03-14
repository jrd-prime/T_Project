using System;
using Code.Core.FSM;
using Code.Core.Managers.UI;

namespace Code.Core.UI.Gameplay.State
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
