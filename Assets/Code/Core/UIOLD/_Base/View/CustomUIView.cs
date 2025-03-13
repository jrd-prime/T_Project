using System;
using Code.Core.Providers.Localization;
using Code.Core.UIOLD._Base.ViewModel;
using Code.Extensions;
using VContainer;

namespace Code.Core.UIOLD._Base.View
{
    public abstract class CustomUIView<TViewModel> : UIViewBase where TViewModel : class, IUIViewModel
    {
        protected IObjectResolver Resolver { get; private set; }
        protected TViewModel ViewModel { get; private set; }
        protected ILocalizationProvider LocalizationProvider { get; private set; }

        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            Resolver = resolver;
            ViewModel = Resolver.ResolveAndCheckOnNull<TViewModel>();
            LocalizationProvider = Resolver.ResolveAndCheckOnNull<ILocalizationProvider>();
        }

        private void Start()
        {
            if (ViewModel == null) throw new NullReferenceException("ViewModel is null. " + name);

            InitializeViewElements();
            CreateAndInitComponents();
            Localize();
            InitializeCallbacks();
            RegisterCallbacks();

            IsInitialized = true;
        }
    }
}
