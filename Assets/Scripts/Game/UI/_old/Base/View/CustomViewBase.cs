using System;
using System.Collections.Generic;
using Core.FSM.Data;
using Core.Providers.Localization;
using Game.UI._old.Base.Data;
using Game.UI._old.Base.ViewModel;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Game.UI._old.Base.View
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

        private DiContainer _container;

        [Inject]
        private void Construct(DiContainer container, TViewModel viewModel,
            ILocalizationProvider localizationManager)
        {
            _container = container;
            ViewModel = viewModel;
            LocalizationManager = localizationManager;
        }

        protected void Start()
        {
            foreach (var subState in subViewsData)
            {
                _container.Inject(subState.subView);
                if (!SubViewsCache.TryAdd(subState.subViewType, subState.subView))
                    throw new Exception(
                        $"Subview with subViewType \"{subState.subViewType}\" already added to {name} view");
            }

            if (viewForGameStateType == GameStateType.NotSet)
                throw new Exception("GameStateType for view is not set. " + name);
        }
    }
}
