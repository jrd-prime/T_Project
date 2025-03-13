using Code.Core.WORK.Enums;
using Code.Core.WORK.Enums.States;
using Code.Core.WORK.UIManager;

namespace Code.Core.WORK.GameStates.Menu.State.SubState
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
