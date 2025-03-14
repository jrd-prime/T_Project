using System;
using System.Collections.Generic;
using Code.Core.Managers.Game;
using Code.Core.Providers;
using Code.Core.WORK.UIStates;
using Code.Core.WORK.UIStates.Gameover;
using Code.Core.WORK.UIStates.Gameplay.State;
using Code.Core.WORK.UIStates.Menu;
using Code.Core.WORK.UIStates.Menu.State;
using Code.Core.WORK.UIStates.Pause;
using Code.Core.WORK.UIStates.Win;
using Code.Hero;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Core.WORK.JStateMachine
{
    public interface IJStateMachine : IInitializable, IDisposable
    {
    }

    public class JStateMachine : IJStateMachine
    {
        private readonly Dictionary<GameStateType, IGameState> _states = new();
        private IGameState _currentState = null;
        private IHeroModel _playerModel;
        private IGameManager _gameManager;
        private readonly CompositeDisposable _disposables = new();
        private bool isGameStarted;
        private ISettingsProvider _settingsManager;
        private IStateMachineReactiveAdapter _ra;

        private GameStateType _currentBaseStateType = GameStateType.NotSet;
        private Enum _currentSubState;

        [Inject]
        private void Construct(IObjectResolver container)
        {
            _states.Add(GameStateType.Menu, container.Resolve<MenuState>());
            // _states.Add(GameStateType.GameOver, container.Resolve<UIOverState>());
            // _states.Add(GameStateType.Pause, container.Resolve<PauseState>());
            _states.Add(GameStateType.Gameplay, container.Resolve<GameplayState>());
            // _states.Add(GameStateType.Win, container.Resolve<WinState>());

            _playerModel = container.Resolve<IHeroModel>();
            _gameManager = container.Resolve<IGameManager>();
            _ra = container.Resolve<IStateMachineReactiveAdapter>();

            Debug.LogWarning("_ra " + _ra);
        }

        private void OnNewStateData(StateData stateData)
        {
            var gameStateType = stateData.StateType;

            if (!_states.TryGetValue(gameStateType, out IGameState gameBaseState))
                throw new KeyNotFoundException($"State: {gameStateType} not found!");

            if (IsNewBaseState(gameStateType))
            {
                ChangeBaseState(gameStateType, gameBaseState);
                return;
            }

            gameBaseState.ChangeSubState(stateData.SubState);
            _currentSubState = stateData.SubState;
        }

        private void ChangeBaseState(GameStateType gameStateType, IGameState gameBaseState)
        {
            _currentBaseStateType = gameStateType;
            Debug.LogWarning(
                $"<color=darkblue>[STATE MACHINE]</color> <b>{_currentState?.GetType().Name} >>> {gameStateType}</b>");

            ChangeState(gameBaseState);
        }

        private bool IsNewBaseState(GameStateType gameStateType) =>
            _currentBaseStateType == GameStateType.NotSet || _currentBaseStateType != gameStateType;


        private void ChangeState(IGameState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }

        public void Initialize()
        {
            Debug.Log("<color=darkblue>[STATE MACHINE]</color> Initialize!");
            Debug.LogWarning("_ra " + _ra);
            _gameManager.IsGameRunning
                .Subscribe(value => isGameStarted = value)
                .AddTo(_disposables);
            _ra.StateData
                .Skip(1)
                .Subscribe(OnNewStateData)
                .AddTo(_disposables);
        }
    }
}
