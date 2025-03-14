using Code.Core.WORK.UI.Base.View;
using R3;
using UnityEngine.UIElements;

namespace Code.Core.WORK.UIStates.Menu.View
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
            _head = ContentContainer.Q<Label>(UIElementId.MenuHeaderId);
            _content = ContentContainer.Q<VisualElement>(UIElementId.MenuContentId);

            _playBtn = _content.Q<Button>(UIElementId.PlayBtnId);
            _settingsBtn = _content.Q<Button>(UIElementId.SettingsBtnId);
            _exitBtn = _content.Q<Button>(UIElementId.ExitBtnId);
        }

        protected override void CreateAndInitComponents()
        {
        }

        protected override void Localize()
        {
            // _head.text = LocalizationManager.GetString(headerNameId).ToUpper();

            // _playBtn.text = LocalizationManager.GetString(TextNameId.playBtnNameId).ToUpper();
            // _settingsBtn.text = LocalizationManager.GetString(TextNameId.settingsBtnNameId).ToUpper();
            // _exitBtn.text = LocalizationManager.GetString(TextNameId.exitBtnNameId).ToUpper();
        }

        protected override void InitializeCallbacks()
        {
            CallbacksCache.Add(_playBtn, _ => ViewModel.PlayButtonClicked.OnNext(Unit.Default));
            CallbacksCache.Add(_settingsBtn, _ => ViewModel.SettingsButtonClicked.OnNext(Unit.Default));
            CallbacksCache.Add(_exitBtn, _ => ViewModel.ExitButtonClicked.OnNext(Unit.Default));
        }
    }
}
