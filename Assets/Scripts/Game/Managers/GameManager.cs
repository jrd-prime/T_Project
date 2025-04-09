using Core.Character.Player.Interfaces;
using Core.Managers.Camera.Interfaces;
using Core.Managers.Game.Interfaces;
using Core.Providers;
using Infrastructure.Input;
using ModestTree;
using R3;
using UnityEngine;
using Zenject;

namespace Core.Managers.Game.Impls
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        [Inject] private DiContainer _container;

        private ISettingsProvider SettingsManager;
        private HSM.Impls.HSM _hsm;
        private IGameService _gameService;
        private SignalBus _signalBus;

        public void Initialize()
        {
            Log.Warn("Init game manager");
            _hsm = _container.Resolve<HSM.Impls.HSM>();
            SettingsManager = _container.Resolve<ISettingsProvider>();
            var player = _container.Resolve<IPlayer>();

            _signalBus = _container.Resolve<SignalBus>();
            _gameService = _container.Resolve<IGameService>();

            var cameraManager = _container.Resolve<ICameraManager>();
            cameraManager.SetTarget(player);
        }

        private void Start() => _gameService.StartHSM();

        public void GameOver()
        {
            Debug.LogWarning("<color=red>GAME OVER</color>");
            _gameService.GameOver();
        }

        public void StopTheGame()
        {
            Debug.LogWarning("<color=red>GAME STOPPED</color>");
            _gameService.StopTheGame();
        }

        public void StartNewGame()
        {
            Debug.LogWarning("<color=green>GAME STARTED</color>");
            _gameService.StartNewGame();
            _signalBus.Fire(new EnableInputSignal());
        }

        public void Pause()
        {
            Log.Warn("GAME PAUSED");
            _gameService.Pause();
            _signalBus.Fire(new DisableInputSignal());
            Time.timeScale = 0;
        }

        public void UnPause()
        {
            Log.Warn("GAME UNPAUSED");
            _gameService.UnPause();
            _signalBus.Fire(new EnableInputSignal());
            Time.timeScale = 1;
        }

        public void ContinueGame()
        {
            Log.Warn("GAME CONTINUED");
            _gameService.ContinueGame();
        }

        private void OnDestroy()
        {
        }
    }
}
