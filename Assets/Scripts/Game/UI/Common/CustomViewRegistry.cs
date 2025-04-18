﻿using Game.UI.Interfaces.Model;
using Infrastructure.Localization;
using Zenject;

namespace Game.UI.Common
{
    public abstract class CustomViewRegistry<TViewModel> : AViewRegistryBase where TViewModel : class, IUIViewModel
    {
        protected TViewModel ViewModel { get; private set; }
        protected ILocalizationProvider LocalizationManager;

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
            foreach (var viewBase in ViewsCache)
            {
                _container.Inject(viewBase.Value);
            }
        }
    }
}
