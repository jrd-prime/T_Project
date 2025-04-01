using Core.Managers.HSM.Common;
using Core.Managers.HSM.Interfaces;
using Core.Managers.UI.Interfaces;

namespace Core.Managers.HSM.Impls.States.Gameplay.SubStates_example
{
    public class GameplayMainSubState : BaseState
    {
        public GameplayMainSubState(GameplayState parent, IUIManager uiManager) : base(parent.StateMachine, uiManager)
        {
        }

        public override void Enter()
        {
            UIManager.SwitchToView("main");
        }

        public override void Update()
        {
        }

        public override IState HandleTransition() => null;
    }
}
