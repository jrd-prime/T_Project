using Code.Core.WORK.Enums.States;
using Code.Core.WORK.JStateMachine._Base;
using Code.Core.WORK.UI.Base.Model;

namespace Code.Core.WORK.JStateMachine.State.Win
{
    public sealed class WinState : GameStateBase<IWinModel, EWinSubState>
    {
        protected override void OnBaseStateEnter()
        {
            // UIManager.ShowView(EGameState.Win);
            GameManager.StopTheGame();
        }

        protected override void OnBaseStateExit()
        {
            // UIManager.HideView(EGameState.Win);
        }

        protected override void InitCustomSubscribes()
        {
        }

        protected override void InitializeSubStates()
        {
        }
    }
}
