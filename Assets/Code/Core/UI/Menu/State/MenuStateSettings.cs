using Code.Core.Data;
using Code.Core.FSM;
using Code.Core.Managers.UI;
using Code.Core.UI._Base.ViewStateTypes;

namespace Code.Core.UI.Menu.State
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
