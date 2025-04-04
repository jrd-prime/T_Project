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

        private IState _previousState;
        private readonly Dictionary<Type, IState> _states = new();

        public HSM(SignalBus signalBus, IUIManager uiManager)
        {
            InitializeMainStates(uiManager);

            var rootState = _states[typeof(MenuState)];
            _previousState = null;
            CurrentState = rootState;

            signalBus.Subscribe<ChangeGameStateSignalVo>(OnChangeGameStateSignal);
        }

        /// <summary>
        /// Создаем экземпляры глобальных состояний и регистрируем
        /// </summary>
        private void InitializeMainStates(IUIManager uiManager)
        {
            RegisterState<MenuState>(new MenuState(this, uiManager));
            RegisterState<GameplayState>(new GameplayState(this, uiManager));
        }

        /// <summary>
        /// Запуск дефолтного состояния
        /// </summary>
        public void Start()
        {
            Log.Info($"<color=green>[{nameof(HSM)}]</color> Start with state  {CurrentState.GetType().Name}");
            CurrentState.Enter(_previousState);
        }

        /// <summary>
        /// Обновление состояния
        /// </summary>
        public void Update()
        {
            Log.Warn($"<color=green>[{nameof(HSM)}]</color> Update!");
            CurrentState.Update();
            var nextState = CurrentState.HandleTransition();
            if (nextState != null && nextState != CurrentState) TransitionTo(nextState.GetType());
        }

        /// <summary>
        /// Смена состояния
        /// </summary>
        private void TransitionTo<T>(T stateType) where T : Type
        {
            if (!_states.TryGetValue(stateType, out var newState))
                throw new Exception($"state {stateType.Name} not found");

            Log.Info($"<color=green>[{nameof(HSM)}]</color> {CurrentState.GetType().Name} > {newState.GetType().Name}");
            _previousState = CurrentState;
            CurrentState.Exit(_previousState);
            CurrentState = newState;
            CurrentState.Enter(_previousState);
        }

        private void OnChangeGameStateSignal(ChangeGameStateSignalVo signal) => TransitionTo(signal.StateType);

        /// <summary>
        /// Регистрация глобального состояния
        /// </summary>
        private void RegisterState<T>(IState state) where T : IState => _states[typeof(T)] = state;
    }
}
