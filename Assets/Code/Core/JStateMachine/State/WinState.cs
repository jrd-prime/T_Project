using UnityEngine;

namespace Code.Core.JStateMachine.State
{
    public sealed class WinState : GameStateBase
    {
        public override void Enter()
        {
            Debug.LogWarning("win state enter");

            // UIManager.ShowView(StateType.Win);
        }

        public override void Exit()
        {
            Debug.LogWarning("win state exit");
            // UIManager.HideView(StateType.Win);
        }
    }
}
