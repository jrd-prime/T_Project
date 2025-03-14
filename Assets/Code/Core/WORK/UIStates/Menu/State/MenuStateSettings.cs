using Code.Core.WORK.Enums;
using Code.Core.WORK.JStateMachine;
using Code.Core.WORK.UIManager;
using Code.Core.WORK.UIStates._Base.UIStatesTypes;

namespace Code.Core.WORK.UIStates.Menu.State
{
    public sealed class MenuSettingsUISubState : UISubStateBase
    {
        public MenuSettingsUISubState(IUIManager uiManager, GameStateType baseStateType,
            MenuStateType defaultStateType) : base(
            uiManager, baseStateType, defaultStateType)
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
