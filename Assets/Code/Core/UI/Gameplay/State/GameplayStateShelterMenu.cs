using System;
using Code.Core.Data;
using Code.Core.FSM;
using Code.Core.Managers.UI;

namespace Code.Core.UI.Gameplay.State
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
