using UnityEngine;

namespace Code.Core.JStateMachine.State
{
    public sealed class SettingsState : GameStateBase
    {
        public override void Enter()
        {
            Debug.LogWarning("s ettings state enter");
            // UIManager.ShowView(StateType.Settings);
        }

        public override void Exit()
        {
            Debug.LogWarning("settings state exit");
            // UIManager.HideView(StateType.Settings);
        }
    }
}
