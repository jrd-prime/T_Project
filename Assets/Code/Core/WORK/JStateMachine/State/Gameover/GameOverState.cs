using Code.Core.WORK.Enums.States;
using Code.Core.WORK.JStateMachine._Base;
using Code.Core.WORK.UI.Base.Model;

namespace Code.Core.WORK.JStateMachine.State.Gameover
{
    public sealed class GameOverState : GameStateBase<IGameoverModel, EGameoverSubState>
    {
        protected override void OnBaseStateEnter()
        {
            // UIManager.ShowView(EGameState.GameOver);
            GameManager.GameOver();
        }

        protected override void OnBaseStateExit()
        {
            // UIManager.HideView(EGameState.GameOver);
        }

        protected override void InitCustomSubscribes()
        {
        }

        protected override void InitializeSubStates()
        {
        }
    }
}
