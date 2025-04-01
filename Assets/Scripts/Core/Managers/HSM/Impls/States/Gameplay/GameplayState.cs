using Core.Managers.HSM.Common;
using Core.Managers.HSM.Impls.States.Gameplay.SubStates_example;
using Core.Managers.UI.Interfaces;
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

        public override void Enter()
        {
            UIManager.SetAndShowBaseView(ViewRegistryType.Gameplay);
            base.Enter(); // for substates
        }

        public override void Exit()
        {
            UIManager.HideAllViews();
            base.Exit(); // или перед?
        }
    }
}
