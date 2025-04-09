using System;
using Game.UI.Interfaces.Model;
using Infrastructure.Localization;
using Zenject;

namespace Game.UI.Common
{
    public abstract class CustomViewBase<TUIViewModel> : AViewBase where TUIViewModel : IUIViewModel
    {
        [Inject] protected TUIViewModel ViewModel { get; private set; }
        [Inject] protected ILocalizationProvider LocalizationManager { get; private set; }

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
