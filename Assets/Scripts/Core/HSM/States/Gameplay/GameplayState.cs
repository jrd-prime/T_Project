using Core.HSM.Common;
using Core.HSM.States.Gameplay.SubStates_example;
using Core.Managers.NewUIManager.Interfaces;

namespace Core.HSM.States.Gameplay
{
    public class GameplayState : BaseState
    {
        public GameplayState(HSM stateMachine, IUIManager uiManager) : base(stateMachine, uiManager)
        {
            AddSubState<PlayingSubState>(new PlayingSubState(this, uiManager));
            AddSubState<PausedSubState>(new PausedSubState(this, uiManager));
            AddSubState<InventoryOpenSubState>(new InventoryOpenSubState(this, uiManager));
            SetInitialSubState<PlayingSubState>();
        }

        public override void Enter()
        {
            UIManager.ShowView("GameplayHUD");
            UIManager.SetBaseView("MainGameplayView");
            base.Enter();
        }

        public override void Exit()
        {
            UIManager.HideAllViews();
            UIManager.HideView("GameplayHUD");
            base.Exit();
        }
    }
}
