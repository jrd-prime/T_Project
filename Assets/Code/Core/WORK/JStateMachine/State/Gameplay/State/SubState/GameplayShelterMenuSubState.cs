using System;
using Code.Core.WORK.Enums;
using Code.Core.WORK.Enums.States;
using Code.Core.WORK.JStateMachine.SubState;
using Code.Core.WORK.UIManager;

namespace Code.Core.WORK.JStateMachine.State.Gameplay.State.SubState
{
    public class GameplayShelterMenuSubState : SubStateBase
    {
        public GameplayShelterMenuSubState(IUIManager uiManager, EGameState baseState, Enum defaultSubState) : base(
            uiManager,
            baseState, defaultSubState)
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
