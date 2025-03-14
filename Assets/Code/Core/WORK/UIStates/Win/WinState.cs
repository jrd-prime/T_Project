using Code.Core.WORK.UIStates._Base.Model;
using Code.Core.WORK.UIStates._Base.UIStatesTypes;

namespace Code.Core.WORK.UIStates.Win
{
    public sealed class WinState : UIStateBase<IWinModel, EWinSubState>
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
