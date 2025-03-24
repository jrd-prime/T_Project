using R3;
using Zenject;

namespace Code.Core.Managers.Game
{
    public interface IGameManager : IInitializable
    {
        // public ReactiveProperty<int> PlayerInitialHealth { get; }
        // public ReadOnlyReactiveProperty<int> PlayerHealth { get; }
        public ReactiveProperty<bool> IsGameRunning { get; }
        // public ReactiveProperty<DayTimerDataModel> GameTimeData { get; }


        public void GameOver();

        public void StopTheGame();

        public void StartNewGame();

        public void Pause();

        public void UnPause();
    }
}
