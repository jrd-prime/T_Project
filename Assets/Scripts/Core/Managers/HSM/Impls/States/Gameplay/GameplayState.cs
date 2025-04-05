using Core.Managers.HSM.Common;
using Core.Managers.HSM.Interfaces;
using Core.Managers.UI.Data;
using Core.Managers.UI.Interfaces;
using Game.UI.Data;

namespace Core.Managers.HSM.Impls.States.Gameplay
{
    public class GameplayState : BaseState
    {
        public GameplayState(HSM stateMachine, IUIManager uiManager) : base(stateMachine, uiManager)
        {
        }

        public override void Enter(IState previousState)
        {
            UIManager.ShowView(new UIManagerViewDataVo(ViewRegistryType.Gameplay, ViewIDConst.Main));
        }

        public override void Exit(IState previousState)
        {
            // UIManager.HideAllViews();
        }
    }
}
