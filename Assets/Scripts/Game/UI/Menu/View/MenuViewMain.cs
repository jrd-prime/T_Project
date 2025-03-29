using Core.Extensions;
using Core.Providers.Localization;
using Db.Data;
using Game.UI._Base.View;
using R3;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UI.Menu.View
{
    public class MenuViewMain : CustomSubViewBase<IMenuViewModel>
    {
        private Button _playBtn;
        private Button _settingsBtn;
        private Button _exitBtn;

        private VisualElement _content;

        protected override void InitializeView()
        {
            _content = RootContainer.GetVisualElement<VisualElement>(UIElementId.MenuMainContainerId, name);

            _playBtn = _content.GetVisualElement<Button>(UIElementId.StartBtnId, name);
            _settingsBtn = _content.GetVisualElement<Button>(UIElementId.SettingsBtnId, name);
            _exitBtn = _content.GetVisualElement<Button>(UIElementId.ExitBtnId, name);
        }

        protected override void CreateAndInitComponents()
        {
        }

        protected override void Localize()
        {
            ViewMainHeader.text = LocalizationManager.Localize(LocalizationNameID.Menu).ToUpper();
            _playBtn.text = LocalizationManager.Localize(LocalizationNameID.Start).ToUpper();
            _settingsBtn.text = LocalizationManager.Localize(LocalizationNameID.Settings).ToUpper();
            _exitBtn.text = LocalizationManager.Localize(LocalizationNameID.Exit).ToUpper();
        }

        protected override void InitializeCallbacks()
        {
            CallbacksCache.Add(_playBtn, _ =>
            {
                Debug.LogWarning(" Play Button Clicked");
                ViewModel.StartButtonClicked.OnNext(Unit.Default);
            });
            CallbacksCache.Add(_settingsBtn, _ =>
            {
                Debug.LogWarning(" Settings Button Clicked");
                ViewModel.SettingsButtonClicked.OnNext(Unit.Default);
            });
            CallbacksCache.Add(_exitBtn, _ =>
            {
                Debug.LogWarning(" Exit Button Clicked");
                ViewModel.ExitButtonClicked.OnNext(Unit.Default);
            });
        }
    }
}
