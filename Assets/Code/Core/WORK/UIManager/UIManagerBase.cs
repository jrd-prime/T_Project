using System;
using System.Collections.Generic;
using Code.Core.Providers;
using Code.Core.WORK.Enums;
using Code.Core.WORK.Enums.States;
using Code.Core.WORK.GameStates;
using Code.Core.WORK.UI.Base.Data;
using Code.Core.WORK.UI.Base.View;
using Code.Core.WORK.UIManager.Viewer;
using UnityEngine;
using VContainer;

namespace Code.Core.WORK.UIManager
{
    public abstract class UIManagerBase : MonoBehaviour, IUIManager
    {
        [Header("Viewer"), SerializeField] protected UIViewer viewer;

        [SerializeField] protected List<StateViewData> stateViews = new();

        private ISettingsProvider _settingsManager;
        private IObjectResolver _resolver;
        private bool _isViewsInitialized;
        private EGameState _currentBaseState;
        private Enum _currentSubState;
        private EGameState _previousBaseState;
        private Enum _previousSubState;

        private readonly Dictionary<EGameState, UIViewBase> _viewsCache = new();

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

            if (Enum.GetNames(typeof(EGameState)).Length != stateViews.Count)
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
                _viewsCache.TryAdd(view.uiForState, view.viewHolder);
            }
        }

        public void ShowView(EGameState eGameState, Enum subState, EShowLogic showLogic = EShowLogic.Default)
        {
            _previousBaseState = _currentBaseState;
            _previousSubState = _currentSubState;

            var subViewDto = GetViewData(eGameState, subState);

            switch (showLogic)
            {
                case EShowLogic.Default: DefaultShowLogic(subViewDto); break;
                case EShowLogic.OverSubView: OverShowLogic(eGameState, subViewDto); break;
                case EShowLogic.UnderSubView: UnderShowLogic(eGameState, subViewDto); break;
                default: throw new ArgumentOutOfRangeException(nameof(showLogic), showLogic, null);
            }

            _currentBaseState = eGameState;
            _currentSubState = subState;
        }

        private void DefaultShowLogic(SubViewDto subViewDto) => viewer.ShowNewBase(subViewDto);

        private void UnderShowLogic(EGameState eGameState, SubViewDto subViewDto)
        {
            if (eGameState == _currentBaseState) viewer.ShowUnderSubView(subViewDto);
            else viewer.ShowNewBase(subViewDto);
        }

        private void OverShowLogic(EGameState eGameState, SubViewDto subViewDto)
        {
            if (eGameState == _currentBaseState) viewer.ShowOverSubView(subViewDto);
            else viewer.ShowNewBase(subViewDto);
        }

        private SubViewDto GetViewData(EGameState eGameState, Enum subState)
        {
            if (!_isViewsInitialized) throw new NullReferenceException($"Views not initialized. {name}");

            if (!_viewsCache.TryGetValue(eGameState, out var viewBase))
                throw new KeyNotFoundException($"View not found for state:  {eGameState}. {name}");

            return viewBase.GetSubViewDto(subState);
        }

        public void HideView(EGameState eGameState, Enum subState, EShowLogic showLogic) => viewer.HideView();

        public abstract void ShowPopUpAsync(string clickTimesToExit, int doubleClickDelay);

        public StateData GetPreviousState() => new(_previousBaseState, _previousSubState);
    }
}
