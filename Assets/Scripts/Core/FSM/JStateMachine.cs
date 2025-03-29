using System;
using System.Collections.Generic;
using Core.FSM.Data;
using Core.FSM.Interfaces;
using Core.Managers.Game;
using Core.Providers;
using Game.UI._Base;
using Game.UI.Gameplay.State;
using Game.UI.Menu.State;
using Game.UI.Pause.State;
using R3;
using UnityEngine;
using Zenject;

namespace Core.FSM
{
    // What about HSM?
    public class JStateMachine : IStateMachine
    {
        private IGameState _currentState = null;
        private IGameManager _gameManager;
        private ISettingsProvider _settingsManager;
        private IGameStateDispatcher _ra;

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
        private void Construct(DiContainer container)
        {
            _states.Add(GameStateType.Menu, container.Resolve<MenuState>());
            _states.Add(GameStateType.Gameplay, container.Resolve<GameplayState>());
            _states.Add(GameStateType.Pause, container.Resolve<PauseState>());

            _gameManager = container.Resolve<IGameManager>();
            _ra = container.Resolve<IGameStateDispatcher>();
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

        private void OnNewStateData(StateDataVo stateDataVo)
        {
            var gameStateType = stateDataVo.StateType;

            if (!_states.TryGetValue(gameStateType, out IGameState gameBaseState))
            {
                Debug.LogError(
                    $"<color=red>State: <b>{gameStateType}</b> not found in states cache! Set default state (Menu)</color>");
                _ra.SetStateData(new StateDataVo { StateType = GameStateType.Menu, SubState = MenuStateType.Main });
                return;
            }

            if (IsNewBaseState(gameStateType))
            {
                Debug.LogWarning(
                    $"<color=darkblue>[STATE MACHINE]</color> <b>{_currentState?.GetType().Name} >>> {gameStateType}</b>");

                ChangeState(gameBaseState);
                _currentBaseStateType = gameStateType;
                _currentStateReactive.Value = gameStateType;
                return;
            }

            gameBaseState.ChangeSubState(stateDataVo.SubState);
            _currentSubState = stateDataVo.SubState;
            _currentSubStateReactive.Value = stateDataVo.SubState;
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
