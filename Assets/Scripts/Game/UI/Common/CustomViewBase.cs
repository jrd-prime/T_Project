using System;
using Core.Extensions;
using Core.Providers.Localization;
using Db.Data;
using Game.UI.Common.Base.ViewModel;
using ModestTree;
using UnityEngine.UIElements;
using Zenject;

namespace Game.UI.Common
{
    public abstract class CustomViewBase<TUIViewModel> : AViewBase where TUIViewModel : IUIViewModel
    {
        [Inject] protected TUIViewModel ViewModel { get; private set; }
        [Inject] protected ILocalizationProvider LocalizationManager { get; private set; }

        protected Label ViewMainHeader;

        private void Awake()
        {
            if (template == null) throw new NullReferenceException("Template is null. " + name);

            Template = template.Instantiate();

            RootContainer = Template.GetVisualElement<VisualElement>(UIElementId.ContainerId, name);
            ViewMainHeader = RootContainer.GetVisualElement<Label>(UIElementId.TitleId, name);
            InitializeView();

            Log.Warn("Init view " + GetType());
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
