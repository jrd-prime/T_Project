using System;
using System.Collections.Generic;
using Code.Core.Managers.Game;
using Code.Core.Managers.UI;
using Code.Hero;
using Code.UI._Base.Model;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.UI._Base
{
    public interface IGameState
    {
        public void Enter();
        public void Exit();
        public void ChangeSubState(Enum stateDataSubState);
    }

    public abstract class UIStateBase<TUIModel, TSubStateEnum> : IGameState, IInitializable, IDisposable
        where TUIModel : IUIModel<TSubStateEnum> where TSubStateEnum : Enum
    {
        protected IGameManager GameManager { get; private set; }
        protected IUIManager UIManager { get; private set; }
        protected Dictionary<TSubStateEnum, ISubState> SubStates { get; } = new();
        protected CompositeDisposable Disposables { get; } = new();

        private TUIModel _model;
        private IHeroModel _playerModel;
        private TSubStateEnum _subStateType;
        private ISubState _subState;
        private ISubState _currentSubState;

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

        private void Subscribe()
        {
            InitBaseSubscribes();
            InitCustomSubscribes();
        }

        private void InitBaseSubscribes()
        {
        }

        public void Enter()
        {
            Debug.Log(
                $"<color=red><b>[ENTER BASE]</b></color> {GetType().Name} (Sub: {SubStates.Count} / Disp: {Disposables.Count})");
            OnBaseStateEnter();

            if (!SubStates.TryGetValue(_subStateType, out _subState))
                throw new KeyNotFoundException($"SubState: {_subStateType} not found! Base state: {GetType().Name}");

            _subState.Enter();
            _currentSubState = _subState;
        }

        public void Exit()
        {
            _currentSubState.Exit();
            _currentSubState = null;
            OnBaseStateExit();
            Debug.Log($"<color=red><b>[EXIT BASE]</b></color> {GetType().Name}");
        }

        public void ChangeSubState(Enum stateDataSubState)
        {
            var subState = stateDataSubState == null ? default : (TSubStateEnum)stateDataSubState;
            if (subState == null) throw new NullReferenceException("SubState is null.");

            Debug.Log("<color=darkblue>[CHANGE SUB]</color> To " + subState + " from " +
                      _currentSubState.GetType().Name);

            if (!SubStates.TryGetValue(subState, out _))
                throw new KeyNotFoundException($"SubState: {subState} not found! Base state: {GetType().Name}");

            _currentSubState?.Exit();
            _currentSubState = SubStates[subState];
            SubStates[subState].Enter();
        }

        // protected void ShowMove() => _move.Show();
        // protected void HideMove() => _move.Hide();

        /// <summary>
        /// Initialize SubStates and add to cache <see cref="SubStates"/>
        /// </summary>
        protected abstract void InitializeSubStates();

        protected abstract void InitCustomSubscribes();
        protected abstract void OnBaseStateEnter();
        protected abstract void OnBaseStateExit();
        public void Dispose() => Disposables?.Dispose();
    }
}
