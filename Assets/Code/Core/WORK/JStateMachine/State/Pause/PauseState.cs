using Code.Core.WORK.Enums.States;
using Code.Core.WORK.JStateMachine._Base;
using Code.Core.WORK.UI.Base.Model;

namespace Code.Core.WORK.JStateMachine.State.Pause
{
    public sealed class PauseState : GameStateBase<IPauseModel, EPauseSubState>
    {
        protected override void OnBaseStateEnter()
        {
            // UIManager.ShowView(EGameState.Pause);
            GameManager.Pause();
        }

        protected override void OnBaseStateExit()
        {
            // UIManager.HideView(EGameState.Pause);
            GameManager.UnPause();
        }

        protected override void InitCustomSubscribes()
        {
        }

        protected override void InitializeSubStates()
        {
        }
    }
}
