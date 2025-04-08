using R3;
using Zenject;

namespace Core.Managers.Game.Interfaces
{
    public interface IGameManager : IInitializable
    {
        Observable<bool> IsGameStarted { get; }

        //  ReactiveProperty<int> PlayerInitialHealth { get; }
        //  ReadOnlyReactiveProperty<int> PlayerHealth { get; }
        ReactiveProperty<bool> IsGameRunning { get; }
        //  ReactiveProperty<DayTimerDataModel> GameTimeData { get; }

        void GameOver();
        void StopTheGame();
        void StartNewGame();
        void Pause();
        void UnPause();
        void ContinueGame();
    }
}
