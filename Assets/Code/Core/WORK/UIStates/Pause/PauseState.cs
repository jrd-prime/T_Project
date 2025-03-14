using Code.Core.WORK.UIStates._Base.Model;
using Code.Core.WORK.UIStates._Base.UIStatesTypes;

namespace Code.Core.WORK.UIStates.Pause
{
    public sealed class PauseState : UIStateBase<IPauseModel, EPauseSubState>
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
