using Code.Core.Data;
using Code.Core.Providers.Localization;
using Code.Core.UI._Base.View;
using Code.Extensions;
using UnityEngine.UIElements;

namespace Code.Core.UI.Menu.View
{
    public class MenuViewMain : CustomSubViewBase<IMenuViewModel>
    {
        private Button _playBtn;
        private Button _settingsBtn;
        private Button _exitBtn;

        private Label _head;
        private VisualElement _content;

        protected override void InitializeView()
        {
            _head = ContentContainer.GetVisualElement<Label>(MenuUIElementID.Title, name);

            _content = ContentContainer.GetVisualElement<VisualElement>(UIElementId.MenuContentId, name);

            _playBtn = _content.GetVisualElement<Button>(UIElementId.StartBtnId, name);
            _settingsBtn = _content.GetVisualElement<Button>(UIElementId.SettingsBtnId, name);
            _exitBtn = _content.GetVisualElement<Button>(UIElementId.ExitBtnId, name);
        }

        protected override void CreateAndInitComponents()
        {
        }

        protected override void Localize()
        {
            _head.text = LocalizationManager.Localize(LocalizationNameID.Menu).ToUpper();
            _playBtn.text = LocalizationManager.Localize(LocalizationNameID.Start).ToUpper();
            _settingsBtn.text = LocalizationManager.Localize(LocalizationNameID.Settings).ToUpper();
            _exitBtn.text = LocalizationManager.Localize(LocalizationNameID.Exit).ToUpper();
        }

        protected override void InitializeCallbacks()
        {
            // CallbacksCache.Add(_playBtn, _ => ViewModel.PlayButtonClicked.OnNext(Unit.Default));
            // CallbacksCache.Add(_settingsBtn, _ => ViewModel.SettingsButtonClicked.OnNext(Unit.Default));
            // CallbacksCache.Add(_exitBtn, _ => ViewModel.ExitButtonClicked.OnNext(Unit.Default));
        }
    }
}
