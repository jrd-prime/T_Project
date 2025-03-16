using System;
using System.Collections.Generic;
using Code.Core.Managers.Game;
using Code.Core.Providers;
using Code.Hero;
using Code.UI._Base;
using Code.UI._Base.Data;
using Code.UI.Gameplay.State;
using Code.UI.Menu.State;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Core.FSM
{
    public interface IStateMachine : IInitializable, IDisposable
    {
        public ReadOnlyReactiveProperty<GameStateType> CurrentState { get; }
        public ReadOnlyReactiveProperty<Enum> CurrentSubState { get; }
    }

    // What about HSM?
    public class JStateMachine : IStateMachine
    {
        private IGameState _currentState = null;
        private IHeroModel _playerModel;
        private IGameManager _gameManager;
        private ISettingsProvider _settingsManager;
        private IStateMachineReactiveAdapter _ra;

        private readonly CompositeDisposable _disposables = new();
        private bool isGameStarted;
        private GameStateType _currentBaseStateType = GameStateType.NotSet;
        private Enum _currentSubState;
        private readonly Dictionary<GameStateType, IGameState> _states = new();

        public ReadOnlyReactiveProperty<GameStateType> CurrentState =>
            _currentStateReactive.ToReadOnlyReactiveProperty();

        public ReadOnlyReactiveProperty<Enum> CurrentSubState => _currentSubStateReactive.ToReadOnlyReactiveProperty();

        private readonly ReactiveProperty<GameStateType> _currentStateReactive = new();
        private readonly ReactiveProperty<Enum> _currentSubStateReactive = new();

        [Inject]
        private void Construct(IObjectResolver container)
        {
            _states.Add(GameStateType.Menu, container.Resolve<MenuState>());
            _states.Add(GameStateType.Gameplay, container.Resolve<GameplayState>());

            _playerModel = container.Resolve<IHeroModel>();
            _gameManager = container.Resolve<IGameManager>();
            _ra = container.Resolve<IStateMachineReactiveAdapter>();
        }

        public void Initialize()
        {
            Debug.Log("<color=green>[STATE MACHINE]</color> Initialize!");


            _gameManager.IsGameRunning
                .Subscribe(x => isGameStarted = x)
                .AddTo(_disposables);
            _ra.StateData
                .Skip(1)
                .Subscribe(OnNewStateData)
                .AddTo(_disposables);
        }

        private void OnNewStateData(StateData stateData)
        {
            var gameStateType = stateData.StateType;

            if (!_states.TryGetValue(gameStateType, out IGameState gameBaseState))
                throw new KeyNotFoundException($"State: {gameStateType} not found!");

            if (IsNewBaseState(gameStateType))
            {
                Debug.LogWarning(
                    $"<color=darkblue>[STATE MACHINE]</color> <b>{_currentState?.GetType().Name} >>> {gameStateType}</b>");

                ChangeState(gameBaseState);
                _currentBaseStateType = gameStateType;
                _currentStateReactive.Value = gameStateType;
                return;
            }

            gameBaseState.ChangeSubState(stateData.SubState);
            _currentSubState = stateData.SubState;
            _currentSubStateReactive.Value = stateData.SubState;
        }

        private void ChangeState(IGameState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        private bool IsNewBaseState(GameStateType gameStateType) =>
            _currentBaseStateType == GameStateType.NotSet || _currentBaseStateType != gameStateType;

        public void Dispose() => _disposables?.Dispose();
    }
}
