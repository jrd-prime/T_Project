using System;
using System.Collections.Generic;
using Core.Character.Hero;
using Core.HSM.Interfaces;
using Core.Managers.Game;
using Game.UI._old.Base.Model;
using R3;
using UnityEngine;
using Zenject;
using IUIManager = Core.HSM.IUIManager;

namespace Game.UI
{
    public abstract class UIStateBase<TUIModel, TSubStateEnum> : IState, IDisposable
        where TUIModel : IUIModel<TSubStateEnum>
        where TSubStateEnum : Enum
    {
        protected Dictionary<TSubStateEnum, ISubState> SubStates { get; } = new();
        protected ISubState CurrentSubState;
        protected CompositeDisposable Disposables { get; } = new();

        protected IGameManager GameManager;
        protected IUIManager UIManager;
        private IHeroModel _playerModel;
        private TUIModel _model;

        [Inject]
        private void Construct(IGameManager gameManager, IUIManager uiController, IHeroModel playerModel,
            TUIModel dataModel)
        {
            GameManager = gameManager;
            UIManager = uiController;
            _playerModel = playerModel;
            _model = dataModel;
        }

        public void Initialize()
        {
            if (GameManager == null) throw new NullReferenceException("GameManager is null");
            if (UIManager == null) throw new NullReferenceException("UIManager is null");
            if (_playerModel == null) throw new NullReferenceException("PlayerModel is null");
            if (_model == null) throw new NullReferenceException("DataModel is null");

            InitializeSubStates();
            Subscribe();

            //TODO uncomment
            //if (SubStates.Count == 0) throw new Exception("SubStates is empty. You need to add substates." + this);

            if (Enum.GetNames(typeof(TSubStateEnum)).Length != SubStates.Count)
                Debug.Log(
                    $"<color=red>[{GetType().Name} / SUB STATES] Initialized substates: {SubStates.Count} but should be {Enum.GetNames(typeof(TSubStateEnum)).Length}</color>");
        }

        protected void RegisterSubState(TSubStateEnum stateType, ISubState subState) =>
            SubStates.TryAdd(stateType, subState);

        public void ChangeSubState(TSubStateEnum newSubState)
        {
            if (!SubStates.TryGetValue(newSubState, out var nextState))
                throw new KeyNotFoundException($"SubState {newSubState} not found in {GetType().Name}");

            CurrentSubState?.Exit();
            CurrentSubState = nextState;
            CurrentSubState.Enter();
        }

        public void Enter()
        {
            Debug.Log($"Entering {GetType().Name}");
            OnEnter();
            CurrentSubState?.Enter();
        }

        public void Exit()
        {
            CurrentSubState?.Exit();
            OnExit();
            Debug.Log($"Exiting {GetType().Name}");
        }

        public void Update()
        {
        }

        public IState HandleTransition()
        {
            return null;
        }

        protected abstract void InitializeSubStates();
        protected abstract void OnEnter();
        protected abstract void OnExit();
        protected abstract void Subscribe();
        public void Dispose() => Disposables.Dispose();
    }

    public interface ISubState
    {
        void Exit();
        void Enter();
    }
}
