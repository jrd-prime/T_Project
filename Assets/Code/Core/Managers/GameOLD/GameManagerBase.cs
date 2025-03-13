using Code.Core.JStateMachineOLD;
using Code.Core.UIOLD.Views.Menu;
using Code.Extensions;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Core.Managers.GameOLD
{
    public abstract class GameManagerBase : MonoBehaviour, IInitializable
    {
        public ReactiveProperty<int> PlayerInitialHealth { get; } = new();

        // public ReadOnlyReactiveProperty<int> PlayerHealth => PlayerModel.Health;
        // public ReadOnlyReactiveProperty<int> KillCount => EnemiesManager.Kills;
        // public ReadOnlyReactiveProperty<int> KillToWin => EnemiesManager.KillToWin;
        // public ReadOnlyReactiveProperty<int> EnemiesCount => EnemiesManager.EnemiesCount;
        // public ReadOnlyReactiveProperty<int> Level => ExperienceManager.Level;
        // public ReadOnlyReactiveProperty<int> Experience => ExperienceManager.Experience;
        // public ReadOnlyReactiveProperty<int> ExperienceToNextLevel => ExperienceManager.ExperienceToNextLevel;
        public ReactiveProperty<bool> IsGameStarted { get; } = new();

        protected IGameStateMachine StateMachine { get; private set; }

        // protected IEnemiesManager EnemiesManager;

        // protected IPlayerModel PlayerModel;

        // protected UIManager UIManager;

        // protected IExperienceManager ExperienceManager;
        protected bool IsGamePaused;
        protected int SpawnDelay;
        protected int MinEnemiesOnMap;
        protected int MaxEnemiesOnMap;
        protected int KillsToWin;

        private IObjectResolver _resolver;
        private IMenuUIModel menuModel;


        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            Debug.LogWarning("construct game manager");
            _resolver = resolver;
            StateMachine = _resolver.ResolveAndCheckOnNull<IGameStateMachine>();
            StateMachine.ChangeStateTo(StateType.Menu);
            menuModel = _resolver.ResolveAndCheckOnNull<IMenuUIModel>();
        }

        private void Start()
        {
            Debug.LogWarning("menu model  " + menuModel);
            menuModel.OnStartButtonClicked.Subscribe(x =>
            {
                Debug.LogWarning("start button clicked");
                StateMachine.ChangeStateTo(StateType.Gameplay);
            });
        }

        protected void Awake()
        {
            // EnemiesManager = ResolverHelp.ResolveAndCheck<IEnemiesManager>(_resolver);
            // PlayerModel = ResolverHelp.ResolveAndCheck<IPlayerModel>(_resolver);
            // UIManager = ResolverHelp.ResolveAndCheck<UIManager>(_resolver);
            // ExperienceManager = ResolverHelp.ResolveAndCheck<IExperienceManager>(_resolver);
            // _settingsManager = ResolverHelp.ResolveAndCheck<ISettingsManager>(_resolver);

            // var gameSettings = _settingsManager.GetConfig<GameSettings>();
            // if (gameSettings == null) throw new NullReferenceException($"Game settings is null. {this}");
            //
            // SpawnDelay = gameSettings.spawnDelay;
            // MinEnemiesOnMap = gameSettings.minEnemiesOnMap;
            // MaxEnemiesOnMap = gameSettings.maxEnemiesOnMap;
            // KillsToWin = gameSettings.killsToWin;
            //
            // PlayerInitialHealth.Value = PlayerModel.CharSettings.health;
        }

        public void Initialize()
        {
        }
    }
}
