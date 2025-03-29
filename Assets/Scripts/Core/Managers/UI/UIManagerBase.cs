using System;
using System.Collections.Generic;
using Core.Extensions;
using Core.FSM.Data;
using Core.FSM.Interfaces;
using Core.Managers.UI.Viewer;
using Core.Providers;
using Db.Data;
using Game.UI._Base.Data;
using Game.UI._Base.View;
using Game.UI.Gameplay.State;
using Game.UI.Pause.State;
using Infrastructure.Input;
using R3;
using UnityEngine;
using Zenject;

namespace Core.Managers.UI
{
    public interface IUIManager : IInitializable
    {
        public void ShowView(GameStateType gameStateType, Enum subState, EShowLogic showLogic = EShowLogic.Default);
        public void HideView(GameStateType gameStateType, Enum subState, EShowLogic showLogic);
        public void ShowPopUpAsync(string clickTimesToExit, int doubleClickDelay);
        public StateDataVo GetPreviousState();
    }

    public abstract class UIManagerBase : MonoBehaviour, IUIManager
    {
        [Header("Viewer"), SerializeField] protected UIViewer viewer;

        [SerializeField] protected List<StateViewData> stateViews = new();


        private DiContainer _container;
        private ISettingsProvider _settingsManager;
        private IJInput _input;

        private bool _isViewsInitialized;

        private GameStateType _currentViewStateType;
        private Enum _currentViewSubState;

        private GameStateType _previousViewStateType;
        private Enum _previousViewSubState;

        private readonly Dictionary<GameStateType, ViewBase> _viewsCache = new();
        private readonly CompositeDisposable _disposables = new();
        private IGameStateDispatcher _ra;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
            _settingsManager = container.ResolveAndCheckOnNull<ISettingsProvider>();
            _input = container.ResolveAndCheckOnNull<IJInput>();
            _ra = container.ResolveAndCheckOnNull<IGameStateDispatcher>();
        }


        public void Initialize()
        {
            InjectAndCacheViews();

            if (stateViews.Count == 0) throw new Exception("Views not found!");

            if (Enum.GetNames(typeof(GameStateType)).Length != stateViews.Count)
                Debug.LogWarning("View count is not equal to game state count");

            _input.OnEscape.Subscribe(EscapeKeyHandler).AddTo(_disposables);

            _isViewsInitialized = true;
        }

        // TODO 
        private void EscapeKeyHandler(Unit _)
        {
            if (_currentViewStateType != GameStateType.Gameplay ||
                !Equals(_currentViewSubState, GameplayStateType.Main)) return;

            _ra.SetStateData(new StateDataVo(GameStateType.Pause, PauseStateType.Main));
        }

        private void Start()
        {
            if (_settingsManager == null) throw new NullReferenceException("SettingsManager is null.");
        }

        private void InjectAndCacheViews()
        {
            foreach (var view in stateViews)
            {
                _container.Inject(view.viewHolder);
                _viewsCache.TryAdd(view.uiForStateType, view.viewHolder);
            }
        }

        public void ShowView(GameStateType gameStateType, Enum subState, EShowLogic showLogic = EShowLogic.Default)
        {
            _previousViewStateType = _currentViewStateType;
            _previousViewSubState = _currentViewSubState;

            var subViewDto = GetViewData(gameStateType, subState);

            switch (showLogic)
            {
                case EShowLogic.Default: DefaultShowLogic(subViewDto); break;
                case EShowLogic.OverSubView: OverShowLogic(gameStateType, subViewDto); break;
                case EShowLogic.UnderSubView: UnderShowLogic(gameStateType, subViewDto); break;
                default: throw new ArgumentOutOfRangeException(nameof(showLogic), showLogic, null);
            }

            _currentViewStateType = gameStateType;
            _currentViewSubState = subState;
        }

        private void DefaultShowLogic(SubViewTemplateData subViewTemplateData) =>
            viewer.ShowNewBase(subViewTemplateData);

        private void UnderShowLogic(GameStateType gameStateType, SubViewTemplateData subViewTemplateData)
        {
            if (gameStateType == _currentViewStateType) viewer.ShowUnderSubView(subViewTemplateData);
            else viewer.ShowNewBase(subViewTemplateData);
        }

        private void OverShowLogic(GameStateType gameStateType, SubViewTemplateData subViewTemplateData)
        {
            if (gameStateType == _currentViewStateType) viewer.ShowOverSubView(subViewTemplateData);
            else viewer.ShowNewBase(subViewTemplateData);
        }

        private SubViewTemplateData GetViewData(GameStateType gameStateType, Enum subState)
        {
            if (!_isViewsInitialized) throw new NullReferenceException($"Views not initialized. {name}");

            if (!_viewsCache.TryGetValue(gameStateType, out var viewBase))
                throw new KeyNotFoundException($"View not found for state:  {gameStateType}. {name}");

            return viewBase.GetSubViewTemplateData(subState);
        }


        public void HideView(GameStateType gameStateType, Enum subState, EShowLogic showLogic) => viewer.HideView();

        public abstract void ShowPopUpAsync(string clickTimesToExit, int doubleClickDelay);

        public StateDataVo GetPreviousState() => new(_previousViewStateType, _previousViewSubState);
        private void OnDestroy() => _disposables.Dispose();
    }
}
