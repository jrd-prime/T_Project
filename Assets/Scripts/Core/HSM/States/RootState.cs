using Core.HSM.Common;
using Core.HSM.States.Menu;

namespace Core.HSM.States
{
    public class RootState : BaseState
    {
        public RootState(HSM stateMachine, IUIManager uiManager) : base(stateMachine, uiManager)
        {
        }


        public override void Enter()
        {
            UIManager.HideAllViews();
            StateMachine.Start();
        }

        public override void Exit()
        {
            UIManager.HideAllViews();
        }
    }
}
