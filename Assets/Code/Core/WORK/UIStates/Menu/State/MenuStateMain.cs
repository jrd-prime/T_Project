using Code.Core.WORK.JStateMachine;
using Code.Core.WORK.UIManager;
using Code.Core.WORK.UIStates._Base.UIStatesTypes;

namespace Code.Core.WORK.UIStates.Menu.State
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
