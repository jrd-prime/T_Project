using System;
using System.Collections.Generic;
using Code.Core.Providers.Localization;
using Code.Core.WORK.JStateMachine;
using Code.Core.WORK.UIStates._Base.Data;
using Code.Core.WORK.UIStates._Base.ViewModel;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace Code.Core.WORK.UIStates._Base.View
{
    public abstract class CustomUIViewBase<TViewModel, TSubStateEnum> : UIViewBase
        where TViewModel : class, IUIViewModel
        where TSubStateEnum : Enum
    {
        [SerializeField] protected TSubStateEnum defaultSubView;
        [SerializeField] protected List<SubViewData<TSubStateEnum>> subViewsData = new();

        protected TViewModel ViewModel { get; private set; }
        protected TSubStateEnum SubStateType { get; private set; }
        protected ILocalizationProvider LocalizationManager;
        protected Dictionary<TSubStateEnum, TemplateContainer> InitializedViewsCache = new();

        private IObjectResolver _resolver;

        [Inject]
        private void Construct(IObjectResolver resolver, TViewModel viewModel,
            ILocalizationProvider localizationManager)
        {
            _resolver = resolver;
            ViewModel = viewModel;
            LocalizationManager = localizationManager;
        }

        protected void Start()
        {
            foreach (var subState in subViewsData)
            {
                _resolver.Inject(subState.subView);
                if (!SubViewsCache.TryAdd(subState.subState, subState.subView))
                    throw new Exception($"Subview with subState \"{subState.subState}\" already added to {name} view");
            }

            if (viewForGameStateType == GameStateType.NotSet)
                throw new Exception("GameStateType for view is not set. " + name);
        }
    }
}
