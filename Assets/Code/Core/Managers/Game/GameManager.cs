using UnityEngine;

namespace Code.Core.Game
{
    public class GameManager : GameManagerBase
    {
        public void GameOver()
        {
            Debug.LogWarning(" Game Over");
            // EnemiesManager.StopSpawn();
            IsGameStarted.Value = false;
        }

        public void StopTheGame()
        {
            Debug.LogWarning(" Game Over");
            // EnemiesManager.StopSpawn();
            IsGameStarted.Value = false;
        }

        public void StartNewGame()
        {
            Debug.LogWarning("start new game");
            if (IsGameStarted.CurrentValue) return;

            IsGameStarted.Value = true;
            // PlayerModel.ResetPlayer();
            // ExperienceManager.ResetExperience();

            // EnemiesManager.StartSpawnEnemiesAsync(KillsToWin, MinEnemiesOnMap, MaxEnemiesOnMap, SpawnDelay);
        }

        public void Pause()
        {
            Debug.LogWarning("pause");
            IsGamePaused = true;
            Time.timeScale = 0;
        }

        public void UnPause()
        {
            Debug.LogWarning("unpause");
            IsGamePaused = false;
            Time.timeScale = 1;
        }
    }
}
