using System;
using Code.Core.WORK.Enums;
using Code.Core.WORK.JStateMachine;
using Code.Core.WORK.UIManager;

namespace Code.Core.WORK.UIStates.Gameplay.State
{
    public class GameplayStateShelterMenu : UISubStateBase
    {
        public GameplayStateShelterMenu(IUIManager uiManager, GameStateType baseStateType, Enum defaultSubState) : base(
            uiManager,
            baseStateType, defaultSubState)
        {
        }

        public override void Enter()
        {
            ShowView(EShowLogic.OverSubView);
        }

        public override void Exit()
        {
            HideView();
        }
    }
}
