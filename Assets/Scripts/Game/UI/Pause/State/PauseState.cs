using Core.FSM.Data;
using Game.UI._Base;

namespace Game.UI.Pause.State
{
    public enum PauseStateType
    {
        Main = 0
    }

    public sealed class PauseState : UIStateBase<IPauseModel, PauseStateType>
    {
        protected override void InitializeSubStates()
        {
            var stateBaseData = new UISubStateBaseData(UIManager, GameStateType.Pause);
            RegisterSubState(PauseStateType.Main, new PauseStateMain(stateBaseData, PauseStateType.Main));
        }

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
    }
}
