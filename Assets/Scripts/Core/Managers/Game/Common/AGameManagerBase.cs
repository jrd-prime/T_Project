using Core.Character.Hero;
using Core.Character.Player;
using Core.Managers.Camera._Game._Scripts.Framework.Manager.JCamera;
using Core.Managers.Game.Interfaces;
using Core.Providers;
using Game.Systems;
using ModestTree;
using R3;
using UnityEngine;
using Zenject;

namespace Core.Managers.Game.Common
{
    public abstract class AGameManagerBase : MonoBehaviour, IGameManager
    {
        public ReactiveProperty<int> PlayerInitialHealth { get; } = new();

        // public ReadOnlyReactiveProperty<int> PlayerHealth => PlayerModel.Health;
        public ReactiveProperty<bool> IsGameRunning { get; } = new(false);
        // public ReactiveProperty<DayTimerDataModel> GameTimeData { get; } = new();

        protected bool IsGamePaused;
        protected IHeroModel PlayerModel;
        protected ISettingsProvider SettingsManager;

        // private GameTimer _gameTimer;

        private readonly CompositeDisposable _disposables = new();

        private HSM.Impls.HSM _hsm;

        // private GameTimerSettings _gameTimerSettings;
        // private IGameCountdownsController _countdownsController;
        [Inject] private DiContainer Container;


        public void Initialize()
        {
            Log.Warn("Init game manager");
            _hsm = Container.Resolve<HSM.Impls.HSM>();
            SettingsManager = Container.Resolve<ISettingsProvider>();
            var player = Container.Resolve<IPlayer>();
            var cameraManager = Container.Resolve<ICameraManager>();

            cameraManager.SetTarget(player);
            InitGameTimer();
        }

        protected void Awake()
        {
            // PlayerInitialHealth.Value = PlayerModel.CharSettings.health;

            // IsGameRunning
            //     .DistinctUntilChanged()
            //     .Subscribe(_gameTimer.SetGameRunningState)
            //     .AddTo(_disposables);

            // _countdownsController.IsDayTimerDataModelLoaded
            //     .DistinctUntilChanged().Where(x => x)
            //     .Take(1)
            //     .Subscribe(_ => _gameTimer.OnModelDataLoaded())
            //     .AddTo(_disposables);
        }

        private void Start()
        {
            Log.Warn("Start game manager");
            _hsm.Start();
        }

        private void InitGameTimer()
        {
            // var timerOptions = new GameTimerOptions
            // {
            //     DayDuration = _gameTimerSettings.gameDayInSeconds,
            //     UpdateInterval = .1f,
            //     CountdownsController = _countdownsController,
            //     MonoBehaviour = this
            // };
            // _gameTimer = new GameTimer(timerOptions).AddTo(_disposables);
            // _container.Inject(_gameTimer);
        }

        public abstract void GameOver();
        public abstract void StopTheGame();
        public abstract void StartNewGame();
        public abstract void Pause();
        public abstract void UnPause();

        private void OnDestroy()
        {
            // _gameTimer?.Dispose();
            _disposables?.Dispose();
            PlayerInitialHealth?.Dispose();
            IsGameRunning?.Dispose();
            // GameTimeData?.Dispose();
        }
    }
}
