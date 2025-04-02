using System;
using System.Collections.Generic;
using Core.Managers.HSM.Impls.States.Gameplay;
using Core.Managers.HSM.Impls.States.Menu;
using Core.Managers.HSM.Interfaces;
using Core.Managers.HSM.Signals;
using Core.Managers.UI.Interfaces;
using ModestTree;
using Zenject;

namespace Core.Managers.HSM.Impls
{
    /// <summary>
    /// Hierarchical State Machine
    /// </summary>
    public sealed class HSM
    {
        private IState _currentState;
        private readonly Dictionary<Type, IState> _states = new();

        public HSM(SignalBus signalBus, IUIManager uiManager)
        {
            InitializeMainStates(uiManager);

            var rootState = _states[typeof(MenuState)];
            _currentState = rootState;

            signalBus.Subscribe<ChangeGameStateSignalVo>(OnChangeGameStateSignal);
        }

        private void OnChangeGameStateSignal(ChangeGameStateSignalVo signal) => TransitionTo(signal.StateType);

        private void InitializeMainStates(IUIManager uiManager)
        {
            RegisterState<MenuState>(new MenuState(this, uiManager));
            RegisterState<GameplayState>(new GameplayState(this, uiManager));
        }

        public void Start() => _currentState.Enter();

        public void Update()
        {
            Log.Info("hsm updated");
            _currentState.Update();
            var nextState = _currentState.HandleTransition();
            if (nextState != null && nextState != _currentState) TransitionTo(nextState);
        }

        public void TransitionTo<T>(T stateType) where T : Type
        {
            Log.Info($"hsm transition to {stateType}");
            if (_states.TryGetValue(stateType, out IState newState)) TransitionTo(newState);
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
