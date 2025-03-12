using System;
using System.Collections.Generic;
using Code.Core.GameStateMachine.State;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Core.GameStateMachine
{
    public enum StateType
    {
        Gameplay,
        Menu,
        PauseMenu,
        Inventory,
        Settings,
        Shop,
        Map,
        QuestLog,
        CharacterScreen,
        Crafting,
        Dialog,
        GameOver,
        Win
    }

    public interface IGameStateMachine
    {
        public ReadOnlyReactiveProperty<StateType> GameState { get; }
        public void ChangeStateTo(StateType stateType);
    }

    public class StateMachine : IGameStateMachine, IInitializable, IDisposable
    {
        public ReadOnlyReactiveProperty<StateType> GameState { get; private set; }

        private readonly Dictionary<StateType, IGameState> _states = new();

        private IGameState _currentState;
        private readonly ReactiveProperty<StateType> _gameState = new(StateType.Menu);

        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            _states.Add(StateType.PauseMenu, resolver.Resolve<PauseMenuState>());
            _states.Add(StateType.GameOver, resolver.Resolve<GameOverState>());
            _states.Add(StateType.Menu, resolver.Resolve<MenuState>());
            _states.Add(StateType.Gameplay, resolver.Resolve<GamePlayState>());
            // _states.Add(StateType.Settings, resolver.Resolve<SettingsState>());
            _states.Add(StateType.Win, resolver.Resolve<WinState>());
        }

        public void Initialize()
        {
            Debug.LogWarning("initialize state machine");
            GameState = _gameState.ToReadOnlyReactiveProperty();
        }

        public void ChangeStateTo(StateType stateType)
        {
            if (!_states.TryGetValue(stateType, out IGameState state))
                throw new KeyNotFoundException($"State: {stateType} not found!");

            ChangeState(state);
            Debug.Log(
                $"- StateMachine - / Change state to: {stateType} / Current state: {_currentState.GetType().Name}");
        }

        private void ChangeState(IGameState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public void Dispose()
        {
        }
    }
}
