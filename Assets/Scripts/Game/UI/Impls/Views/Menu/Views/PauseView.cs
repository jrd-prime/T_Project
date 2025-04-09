using Game.Extensions;
using Game.UI.Common;
using Game.UI.Data;
using Infrastructure.Localization;
using UnityEngine.UIElements;

namespace Game.UI.Impls.Views.Menu.Views
{
    public sealed class PauseView : CustomViewBase<IMenuViewModel>
    {
        private Button _continueBtn;
        private Button _settingsBtn;
        private Button _menuBtn;

        protected override void InitializeView()
        {
            _continueBtn = ContentContainer.GetVisualElement<Button>(UIElementId.ContinueBtnId, name);
            _settingsBtn = ContentContainer.GetVisualElement<Button>(UIElementId.SettingsBtnId, name);
            _menuBtn = ContentContainer.GetVisualElement<Button>(UIElementId.MenuBtnId, name);
        }

        protected override void CreateAndInitComponents()
        {
        }

        protected override void Localize()
        {
            ViewMainHeader.text = LocalizationManager.Localize(LocalizationNameID.Pause).ToUpper();
            _continueBtn.text = LocalizationManager.Localize(LocalizationNameID.Continue).ToUpper();
            _settingsBtn.text = LocalizationManager.Localize(LocalizationNameID.Settings).ToUpper();
            _menuBtn.text = LocalizationManager.Localize(LocalizationNameID.ToMainMenu).ToUpper();
        }

        protected override void InitializeCallbacks()
        {
            CallbacksCache.Add(_continueBtn, _ => ViewModel.ContinueBtnClicked());
            CallbacksCache.Add(_settingsBtn, _ => ViewModel.SettingsBtnClicked());
            CallbacksCache.Add(_menuBtn, _ => ViewModel.MenuBtnClicked());
        }
    }
}
