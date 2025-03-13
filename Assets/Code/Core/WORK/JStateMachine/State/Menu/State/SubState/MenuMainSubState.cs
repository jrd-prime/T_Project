using Code.Core.WORK.Enums.States;
using Code.Core.WORK.JStateMachine.SubState;
using Code.Core.WORK.UIManager;

namespace Code.Core.WORK.JStateMachine.State.Menu.State.SubState
{
    public class MenuMainSubState : SubStateBase
    {
        public MenuMainSubState(IUIManager uiManager, EGameState baseState, EMenuSubState defaultSubState) : base(
            uiManager, baseState, defaultSubState)
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
