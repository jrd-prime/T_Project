using Core.Managers.HSM.Common;
using Core.Managers.HSM.Interfaces;
using Core.Managers.UI.Interfaces;
using Game.UI.Common;
using Game.UI.Data;

namespace Core.Managers.HSM.Impls.States.Gameplay.SubStates_example
{
    public class GameplayMainSubState : BaseState
    {
        public GameplayMainSubState(GameplayState parent, IUIManager uiManager) : base(parent.StateMachine, uiManager)
        {
        }

        public override void Enter(IState previousState)
        {
        }

        public override void Update()
        {
        }

        public override IState HandleTransition() => null;
    }
}
