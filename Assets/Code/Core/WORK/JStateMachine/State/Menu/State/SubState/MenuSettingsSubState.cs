using Code.Core.WORK.Enums;
using Code.Core.WORK.Enums.States;
using Code.Core.WORK.JStateMachine.SubState;
using Code.Core.WORK.UIManager;

namespace Code.Core.WORK.JStateMachine.State.Menu.State.SubState
{
    public sealed class MenuSettingsSubState : SubStateBase
    {
        public MenuSettingsSubState(IUIManager uiManager, EGameState baseState, EMenuSubState defaultSubState) : base(
            uiManager, baseState, defaultSubState)
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
