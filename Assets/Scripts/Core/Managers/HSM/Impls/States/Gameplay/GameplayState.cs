using Core.Managers.HSM.Common;
using Core.Managers.HSM.Impls.States.Gameplay.SubStates_example;
using Core.Managers.HSM.Interfaces;
using Core.Managers.UI.Interfaces;
using Game.UI.Common;
using Game.UI.Data;

namespace Core.Managers.HSM.Impls.States.Gameplay
{
    public class GameplayState : BaseState
    {
        public GameplayState(HSM stateMachine, IUIManager uiManager) : base(stateMachine, uiManager)
        {
            AddSubState<GameplayMainSubState>(new GameplayMainSubState(this, uiManager));
            AddSubState<PausedSubState>(new PausedSubState(this, uiManager));
            AddSubState<InventoryOpenSubState>(new InventoryOpenSubState(this, uiManager));

            SetInitialSubState<GameplayMainSubState>();
        }

        public override void Enter(IState previousState)
        {
            UIManager.SetAndShowBaseView(ViewRegistryType.Gameplay, ViewIDConst.Main);
            base.Enter(previousState); // for substates
        }

        public override void Exit(IState previousState)
        {
            UIManager.HideAllViews();
            base.Exit(previousState); // или перед?
        }
    }
}
