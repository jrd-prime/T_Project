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
        public IState CurrentState { get; private set; }
        private readonly Dictionary<Type, IState> _states = new();

        public HSM(SignalBus signalBus, IUIManager uiManager)
        {
            InitializeMainStates(uiManager);

            var rootState = _states[typeof(MenuState)];
            PreviousState = null;
            CurrentState = rootState;

            signalBus.Subscribe<ChangeGameStateSignalVo>(OnChangeGameStateSignal);
        }

        private void OnChangeGameStateSignal(ChangeGameStateSignalVo signal) => TransitionTo(signal.StateType);

        private void InitializeMainStates(IUIManager uiManager)
        {
            RegisterState<MenuState>(new MenuState(this, uiManager));
            RegisterState<GameplayState>(new GameplayState(this, uiManager));
        }

        public void Start() => CurrentState.Enter(PreviousState);

        public void Update()
        {
            Log.Info("hsm updated");
            CurrentState.Update();
            var nextState = CurrentState.HandleTransition();
            if (nextState != null && nextState != CurrentState) TransitionTo(nextState);
        }

        public void TransitionTo<T>(T stateType) where T : Type
        {
            Log.Info($"hsm transition to {stateType}");
            if (_states.TryGetValue(stateType, out IState newState)) TransitionTo(newState);
        }

        private void TransitionTo(IState newState)
        {
            Log.Info($"hsm transition to {newState.GetType().Name}");
            PreviousState = CurrentState;
            CurrentState.Exit(PreviousState);
            CurrentState = newState;
            CurrentState.Enter(PreviousState);
        }

        public IState PreviousState { get; private set; }

        private void RegisterState<T>(IState state) where T : IState => _states[typeof(T)] = state;
    }
}
