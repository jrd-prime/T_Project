using Core.Providers.Localization;
using Game.UI._old.Base.ViewModel;
using Game.UI.Common;
using Zenject;

namespace Game.UI._old.Base.View
{
    public abstract class CustomViewBase<TViewModel> : ViewBase where TViewModel : class, IUIViewModel
    {
        protected TViewModel ViewModel { get; private set; }
        protected ILocalizationProvider LocalizationManager;
        // protected Dictionary<TSubViewType, TemplateContainer> InitializedViewsCache = new();

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
            foreach (var viewBase in SubViewsCache)
            {
                _container.Inject(viewBase.Value);
            }
        }
    }
}
