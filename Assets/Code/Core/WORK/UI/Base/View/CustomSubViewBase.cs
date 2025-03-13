using System;
using Code.Core.Providers.Localization;
using Code.Core.WORK.UI.Base.ViewModel;
using Code.Extensions;
using UnityEngine.UIElements;
using VContainer;

namespace Code.Core.WORK.UI.Base.View
{
    public abstract class CustomSubViewBase<T> : JSubViewBase where T : IUIViewModel
    {
        protected T ViewModel { get; private set; }
        protected ILocalizationProvider LocalizationManager { get; private set; }

        [Inject]
        private void Construct(T viewModel, ILocalizationProvider localizationManager)
        {
            ViewModel = viewModel;
            LocalizationManager = localizationManager;
        }

        private void Awake()
        {
            if (template == null) throw new NullReferenceException("Template is null. " + name);

            Template = template.Instantiate();

            ContentContainer = Template.GetVisualElement<VisualElement>(UIElementId.MainContentContainerId, name);

            InitializeView();

            IsInitialized = true;
        }

        private void Start()
        {
            if (ViewModel == null) throw new NullReferenceException("ViewModel is null. " + name);
            if (LocalizationManager == null) throw new NullReferenceException("LocalizationManager is null. " + name);

            CreateAndInitComponents();
            Localize();
            InitializeCallbacks();
            RegisterCallbacks();
        }
    }
}
