using System;
using System.Collections.Generic;
using Core.Managers.HSM.Interfaces;
using Core.Managers.UI.Interfaces;
using ModestTree;

namespace Core.Managers.HSM.Common
{
    public abstract class BaseState : IState
    {
        protected internal readonly Impls.HSM StateMachine;
        protected readonly IUIManager UIManager;
        protected IState CurrentSubState;
        protected readonly Dictionary<Type, IState> SubStates = new();

        protected BaseState(Impls.HSM stateMachine, IUIManager uiManager)
        {
            StateMachine = stateMachine;
            UIManager = uiManager;
        }

        public virtual void Enter()
        {
            Log.Info($"enter state {GetType().Name}");
            if (CurrentSubState != null) CurrentSubState.Enter();
        }

        public virtual void Exit()
        {
            Log.Info($"exit state {GetType().Name}");
            if (CurrentSubState != null) CurrentSubState.Exit();
        }

        public virtual void Update()
        {
            if (CurrentSubState == null) return;

            CurrentSubState.Update();
            var nextSubState = CurrentSubState.HandleTransition();
            if (nextSubState != null && nextSubState != CurrentSubState)
                TransitionToSubState(nextSubState);
        }

        public virtual IState HandleTransition() => null;
        protected void AddSubState<T>(IState state) where T : IState => SubStates[typeof(T)] = state;
        protected void SetInitialSubState<T>() where T : IState => CurrentSubState = SubStates[typeof(T)];

        protected void TransitionToSubState<T>() where T : IState
        {
            if (SubStates.TryGetValue(typeof(T), out IState newState)) TransitionToSubState(newState);
        }

        private void TransitionToSubState(IState newState)
        {
            if (newState == CurrentSubState) return;

            CurrentSubState?.Exit();
            CurrentSubState = newState;
            CurrentSubState?.Enter();
        }
    }
}
