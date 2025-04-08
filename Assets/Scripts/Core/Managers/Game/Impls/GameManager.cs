using Core.Managers.Game.Common;
using Infrastructure.Input;
using ModestTree;
using UnityEngine;

namespace Core.Managers.Game.Impls
{
    public class GameManager : AGameManagerBase
    {
        public override void GameOver()
        {
            Debug.LogWarning("<color=red>GAME OVER</color>");
            IsGameRunning.Value = false;
        }

        public override void StopTheGame()
        {
            Debug.LogWarning("<color=red>GAME STOPPED</color>");
            IsGameRunning.Value = false;
        }

        public override void StartNewGame()
        {
            if (IsGameRunning.CurrentValue) return;

            Debug.LogWarning("<color=green>GAME STARTED</color>");
            SetGameStarted();

            SignalBus.Fire(new EnableInputSignal());

            IsGameRunning.Value = true;
            IsGamePaused = false;
            // PlayerModel.ResetPlayer();
        }

        public override void Pause()
        {
            Log.Warn("GAME PAUSED");
            SignalBus.Fire(new DisableInputSignal());
            IsGameRunning.Value = false;
            IsGamePaused = true;
            Time.timeScale = 0;
        }

        public override void UnPause()
        {
            Log.Warn("GAME UNPAUSED");

            SignalBus.Fire(new EnableInputSignal());
            IsGameRunning.Value = true;
            IsGamePaused = false;
            Time.timeScale = 1;
        }

        public override void ContinueGame()
        {
            Log.Warn("GAME CONTINUED");
        }
    }
}
