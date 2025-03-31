using System;
using System.Collections.Generic;
using Core.HSM.Interfaces;
using Core.HSM.States.Gameplay;
using Core.HSM.States.Menu;
using Core.Managers.UI.Interfaces;
using ModestTree;
using UnityEngine;

namespace Core.HSM
{
    /// <summary>
    /// Hierarchical State Machine
    /// </summary>
    public sealed class HSM
    {
        private IState _currentState;
        private readonly Dictionary<Type, IState> _states = new();

        public HSM(IUIManager uiManager)
        {
            InitializeMainStates(uiManager);

            var rootState = _states[typeof(MenuState)];
            _currentState = rootState;
        }

        private void InitializeMainStates(IUIManager uiManager)
        {
            RegisterState<MenuState>(new MenuState(this, uiManager));
            RegisterState<GameplayState>(new GameplayState(this, uiManager));
        }

        public void Start()
        {
            Log.Info("hsm started");
            _currentState.Enter();
        }

        public void Update()
        {
            Log.Info("hsm updated");
            _currentState.Update();
            var nextState = _currentState.HandleTransition();
            if (nextState != null && nextState != _currentState) TransitionTo(nextState);
        }

        public void TransitionTo<T>() where T : IState
        {
            Log.Info($"hsm transition to {typeof(T).Name}");
            if (_states.TryGetValue(typeof(T), out IState newState)) TransitionTo(newState);
        }

        private void TransitionTo(IState newState)
        {
            Log.Info($"hsm transition to {newState.GetType().Name}");
            _currentState.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        private void RegisterState<T>(IState state) where T : IState => _states[typeof(T)] = state;
    }
}
