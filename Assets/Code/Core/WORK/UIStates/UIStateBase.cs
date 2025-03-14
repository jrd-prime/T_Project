using System;
using System.Collections.Generic;
using Code.Core.Managers.Game;
using Code.Core.WORK.UI.Base.Model;
using Code.Core.WORK.UIManager;
using Code.Hero;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Core.WORK.UIStates
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
        protected readonly Dictionary<TSubStateEnum, ISubState> SubStatesCache = new();

        private TUIModel _model;
        private IHeroModel _playerModel;
        protected readonly CompositeDisposable Disposables = new();
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
            //if (SubStatesCache.Count == 0) throw new Exception("SubStates is empty. You need to add substates." + this);

            if (Enum.GetNames(typeof(TSubStateEnum)).Length != SubStatesCache.Count)
                Debug.Log(
                    $"<color=red>[{GetType().Name} / SUB STATES] Initialized substates: {SubStatesCache.Count} but should be {Enum.GetNames(typeof(TSubStateEnum)).Length}</color>");
        }

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
                $"<color=red><b>[ENTER BASE]</b></color> {GetType().Name} (Sub: {SubStatesCache.Count} / Disp: {Disposables.Count})");
            OnBaseStateEnter();

            if (!SubStatesCache.TryGetValue(_subStateType, out _subState))
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

            _currentSubState?.Exit();
            _currentSubState = SubStatesCache[subState];
            SubStatesCache[subState].Enter();
        }

        // protected void ShowMove() => _move.Show();
        // protected void HideMove() => _move.Hide();

        /// <summary>
        /// Initialize SubStates and add to cache <see cref="SubStatesCache"/>
        /// </summary>
        protected abstract void InitializeSubStates();

        protected abstract void InitCustomSubscribes();
        protected abstract void OnBaseStateEnter();
        protected abstract void OnBaseStateExit();

        public void Dispose()
        {
            Disposables?.Dispose();
        }
    }
}
