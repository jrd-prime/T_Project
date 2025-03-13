using System;
using System.Collections.Generic;
using Code.Core.Providers;
using Code.Core.WORK.Enums.States;
using Code.Core.WORK.Game;
using Code.Core.WORK.GameStates;
using Code.Core.WORK.GameStates.Gameover;
using Code.Core.WORK.GameStates.Gameplay.State;
using Code.Core.WORK.GameStates.Menu.State;
using Code.Core.WORK.GameStates.Pause;
using Code.Core.WORK.GameStates.Win;
using Code.Hero;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Core.WORK.JStateMachine
{
    public interface IStateMachine : IStartable, IDisposable
    {
    }

    public class StateMachine : IStateMachine
    {
        private readonly Dictionary<EGameState, IGameState> _states = new();
        private IGameState _currentState = null;
        private IHeroModel _playerModel;
        private IGameManager _gameManager;
        private readonly CompositeDisposable _disposables = new();
        private bool isGameStarted;
        private ISettingsProvider _settingsManager;
        private IStateMachineReactiveAdapter _ra;

        private EGameState _currentBaseState;
        private Enum _currentSubState;

        [Inject]
        private void Construct(IObjectResolver container)
        {
            _states.Add(EGameState.Menu, container.Resolve<MenuState>());
            _states.Add(EGameState.GameOver, container.Resolve<GameOverState>());
            _states.Add(EGameState.Pause, container.Resolve<PauseState>());
            _states.Add(EGameState.Gameplay, container.Resolve<GamePlayState>());
            _states.Add(EGameState.Win, container.Resolve<WinState>());

            _playerModel = container.Resolve<IHeroModel>();
            _gameManager = container.Resolve<GameManager>();
            _ra = container.Resolve<IStateMachineReactiveAdapter>();
        }

        public void Start()
        {
            Debug.Log("<color=darkblue>[STATE MACHINE]</color> Start!");

            if (_currentState != null) return;

            var defStateData = new StateData { State = EGameState.Menu, SubState = default };

            ChangeBaseState(defStateData);

            _gameManager.IsGameRunning
                .Subscribe(value => isGameStarted = value)
                .AddTo(_disposables);
            _ra.StateData
                .Skip(1)
                .Subscribe(OnNewStateData)
                .AddTo(_disposables);
        }

        private void OnNewStateData(StateData stateData)
        {
            if (_currentBaseState != stateData.State)
            {
                ChangeBaseState(stateData);
                _currentBaseState = stateData.State;
            }
            else
            {
                if (!_states.TryGetValue(stateData.State, out var state))
                    throw new KeyNotFoundException($"State: {stateData.State} not found!");

                state.ChangeSubState(stateData.SubState);
                _currentSubState = stateData.SubState;
            }
        }

        private void ChangeBaseState(StateData stateData)
        {
            if (!_states.TryGetValue(stateData.State, out IGameState state))
                throw new KeyNotFoundException($"State: {stateData.State} not found!");
            Debug.LogWarning(
                $"<color=darkblue>[STATE MACHINE]</color> <b>{_currentState?.GetType().Name} >>> {stateData.State}</b>");

            ChangeState(state);
            // state.ChangeSubState(stateData.SubState);
        }

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
    }
}
