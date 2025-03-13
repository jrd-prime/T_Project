namespace Code.Core.JStateMachine.State
{
    public sealed class GameOverState : GameStateBase
    {
        public override void Enter()
        {
            // UIManager.ShowView(StateType.GameOver);
        }

        public override void Exit()
        {
            // UIManager.HideView(StateType.GameOver);
        }
    }
}
