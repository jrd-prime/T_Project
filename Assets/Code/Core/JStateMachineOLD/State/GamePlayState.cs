namespace Code.Core.JStateMachineOLD.State
{
    public sealed class GamePlayState : GameStateBase
    {
        public override void Enter()
        {
            // UIManager.ShowView(StateType.Game);
            // PlayerModel.SetGameStarted(true);
        }

        public override void Exit()
        {
            // UIManager.HideView(StateType.Game);
            // PlayerModel.SetGameStarted(false);
        }
    }
}
