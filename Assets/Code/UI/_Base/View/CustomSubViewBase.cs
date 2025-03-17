using System;
using Code.Core.Data;
using Code.Core.Providers.Localization;
using Code.Extensions;
using Code.UI._Base.ViewModel;
using UnityEngine.UIElements;
using VContainer;

namespace Code.UI._Base.View
{
    public abstract class CustomSubViewBase<TUIViewModel> : SubViewBase where TUIViewModel : IUIViewModel
    {
        protected TUIViewModel ViewModel { get; private set; }
        protected ILocalizationProvider LocalizationManager { get; private set; }

        protected Label ViewMainHeader;

        [Inject]
        private void Construct(TUIViewModel viewModel, ILocalizationProvider localizationManager)
        {
            ViewModel = viewModel;
            LocalizationManager = localizationManager;
        }

        private void Awake()
        {
            if (template == null) throw new NullReferenceException("Template is null. " + name);

            Template = template.Instantiate();

            RootContainer = Template.GetVisualElement<VisualElement>(UIElementId.ContainerId, name);
            ViewMainHeader = RootContainer.GetVisualElement<Label>(UIElementId.TitleId, name);
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
