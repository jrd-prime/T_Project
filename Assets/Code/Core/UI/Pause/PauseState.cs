using Code.Core.UI._Base.Model;
using Code.Core.UI._Base.ViewStateTypes;

namespace Code.Core.UI.Pause
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
