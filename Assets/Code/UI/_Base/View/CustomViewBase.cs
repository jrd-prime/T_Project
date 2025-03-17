using System;
using System.Collections.Generic;
using Code.Core.FSM;
using Code.Core.Providers.Localization;
using Code.UI._Base.Data;
using Code.UI._Base.ViewModel;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace Code.UI._Base.View
{
    public abstract class CustomViewBase<TViewModel, TSubViewType> : ViewBase
        where TViewModel : class, IUIViewModel
        where TSubViewType : Enum
    {
        [SerializeField] protected TSubViewType defaultSubView;
        [SerializeField] protected List<SubViewData<TSubViewType>> subViewsData = new();

        protected TViewModel ViewModel { get; private set; }
        protected TSubViewType SubStateType { get; private set; }
        protected ILocalizationProvider LocalizationManager;
        protected Dictionary<TSubViewType, TemplateContainer> InitializedViewsCache = new();

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
                if (!SubViewsCache.TryAdd(subState.subViewType, subState.subView))
                    throw new Exception(
                        $"Subview with subViewType \"{subState.subViewType}\" already added to {name} view");
            }

            if (viewForGameStateType == GameStateType.NotSet)
                throw new Exception("GameStateType for view is not set. " + name);
        }
    }
}
