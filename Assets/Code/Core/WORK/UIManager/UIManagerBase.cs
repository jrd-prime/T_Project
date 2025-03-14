using System;
using System.Collections.Generic;
using Code.Core.Providers;
using Code.Core.WORK.Enums;
using Code.Core.WORK.JStateMachine;
using Code.Core.WORK.UIManager.Viewer;
using Code.Core.WORK.UIStates;
using Code.Core.WORK.UIStates._Base.Data;
using Code.Core.WORK.UIStates._Base.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Core.WORK.UIManager
{
    public interface IUIManager : IInitializable
    {
        public void ShowView(GameStateType gameStateType, Enum subState, EShowLogic showLogic = EShowLogic.Default);
        public void HideView(GameStateType gameStateType, Enum subState, EShowLogic showLogic);
        public void ShowPopUpAsync(string clickTimesToExit, int doubleClickDelay);
        public StateData GetPreviousState();
    }

    public abstract class UIManagerBase : MonoBehaviour, IUIManager
    {
        [Header("Viewer"), SerializeField] protected UIViewer viewer;

        [SerializeField] protected List<StateViewData> stateViews = new();

        private ISettingsProvider _settingsManager;
        private IObjectResolver _resolver;
        private bool _isViewsInitialized;
        private GameStateType _currentBaseStateType;
        private Enum _currentSubState;
        private GameStateType _previousBaseStateType;
        private Enum _previousSubState;

        private readonly Dictionary<GameStateType, UIViewBase> _viewsCache = new();

        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            _resolver = resolver;
            _settingsManager = resolver.Resolve<ISettingsProvider>();
        }


        public void Initialize()
        {
            InjectAndCacheViews();

            if (stateViews.Count == 0) throw new Exception("Views not found!");

            if (Enum.GetNames(typeof(GameStateType)).Length != stateViews.Count)
                Debug.LogError("View count is not equal to game state count");

            _isViewsInitialized = true;
        }

        private void Start()
        {
            if (_settingsManager == null) throw new NullReferenceException("SettingsManager is null.");
        }

        private void InjectAndCacheViews()
        {
            foreach (var view in stateViews)
            {
                _resolver.Inject(view.viewHolder);
                _viewsCache.TryAdd(view.uiForStateType, view.viewHolder);
            }
        }

        public void ShowView(GameStateType gameStateType, Enum subState, EShowLogic showLogic = EShowLogic.Default)
        {
            _previousBaseStateType = _currentBaseStateType;
            _previousSubState = _currentSubState;

            var subViewDto = GetViewData(gameStateType, subState);

            switch (showLogic)
            {
                case EShowLogic.Default: DefaultShowLogic(subViewDto); break;
                case EShowLogic.OverSubView: OverShowLogic(gameStateType, subViewDto); break;
                case EShowLogic.UnderSubView: UnderShowLogic(gameStateType, subViewDto); break;
                default: throw new ArgumentOutOfRangeException(nameof(showLogic), showLogic, null);
            }

            _currentBaseStateType = gameStateType;
            _currentSubState = subState;
        }

        private void DefaultShowLogic(SubViewDto subViewDto) => viewer.ShowNewBase(subViewDto);

        private void UnderShowLogic(GameStateType gameStateType, SubViewDto subViewDto)
        {
            if (gameStateType == _currentBaseStateType) viewer.ShowUnderSubView(subViewDto);
            else viewer.ShowNewBase(subViewDto);
        }

        private void OverShowLogic(GameStateType gameStateType, SubViewDto subViewDto)
        {
            if (gameStateType == _currentBaseStateType) viewer.ShowOverSubView(subViewDto);
            else viewer.ShowNewBase(subViewDto);
        }

        private SubViewDto GetViewData(GameStateType gameStateType, Enum subState)
        {
            if (!_isViewsInitialized) throw new NullReferenceException($"Views not initialized. {name}");

            if (!_viewsCache.TryGetValue(gameStateType, out var viewBase))
                throw new KeyNotFoundException($"View not found for state:  {gameStateType}. {name}");

            return viewBase.GetSubViewDto(subState);
        }

        public void HideView(GameStateType gameStateType, Enum subState, EShowLogic showLogic) => viewer.HideView();

        public abstract void ShowPopUpAsync(string clickTimesToExit, int doubleClickDelay);

        public StateData GetPreviousState() => new(_previousBaseStateType, _previousSubState);
    }
}
