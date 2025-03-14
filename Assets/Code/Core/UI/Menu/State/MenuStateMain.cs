using Code.Core.FSM;
using Code.Core.Managers.UI;
using Code.Core.UI._Base.ViewStateTypes;

namespace Code.Core.UI.Menu.State
{
    public class MenuStateMain : UISubStateBase
    {
        public MenuStateMain(IUIManager uiManager, GameStateType baseStateType, MenuStateType defaultStateType) : base(
            uiManager, baseStateType, defaultStateType)
        {
        }

        public override void Enter()
        {
            ShowView();
        }

        public override void Exit()
        {
            // HideView();
        }
    }
}
