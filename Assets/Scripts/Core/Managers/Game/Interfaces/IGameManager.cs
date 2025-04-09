using Zenject;

namespace Core.Managers.Game.Interfaces
{
    public interface IGameManager : IInitializable
    {
        void GameOver();
        void StopTheGame();
        void StartNewGame();
        void Pause();
        void UnPause();
        void ContinueGame();
    }
}
