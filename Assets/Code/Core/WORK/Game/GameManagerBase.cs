using Code.Core.Providers;
using Code.Extensions;
using Code.Hero;
using R3;
using UnityEngine;
using VContainer;

namespace Code.Core.WORK.Game
{
    public abstract class GameManagerBase : MonoBehaviour, IGameManager
    {
        public ReactiveProperty<int> PlayerInitialHealth { get; } = new();

        // public ReadOnlyReactiveProperty<int> PlayerHealth => PlayerModel.Health;
        public ReactiveProperty<bool> IsGameRunning { get; } = new(false);
        // public ReactiveProperty<DayTimerDataModel> GameTimeData { get; } = new();

        protected bool IsGamePaused;
        protected IHeroModel PlayerModel;
        protected ISettingsProvider SettingsManager;

        // private GameTimer _gameTimer;
        private IObjectResolver _resolver;

        private readonly CompositeDisposable _disposables = new();
        // private GameTimerSettings _gameTimerSettings;
        // private IGameCountdownsController _countdownsController;

        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            _resolver = resolver;
            SettingsManager = _resolver.Resolve<ISettingsProvider>();
            // _countdownsController = _resolver.Resolve<IGameCountdownsController>();
            // _gameTimerSettings = SettingsManager.GetConfig<GameTimerSettings>();
        }

        public void Initialize()
        {
            InitGameTimer();
        }

        protected void Awake()
        {
            PlayerModel = _resolver.ResolveAndCheckOnNull<IHeroModel>();

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
            // _resolver.Inject(_gameTimer);
        }

        public abstract void GameOver();
        public abstract void StopTheGame();
        public abstract void StartNewGame();
        public abstract void Pause();
        public abstract void UnPause();

        private void OnDestroy()
        {
            // _gameTimer?.Dispose();
            _resolver?.Dispose();
            _disposables?.Dispose();
            PlayerInitialHealth?.Dispose();
            IsGameRunning?.Dispose();
            // GameTimeData?.Dispose();
        }
    }
}
