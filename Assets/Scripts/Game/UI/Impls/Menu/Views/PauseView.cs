using Core.Extensions;
using Core.Providers.Localization;
using Db.Data;
using Game.UI.Common;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UI.Impls.Menu.Views
{
    public sealed class PauseView : CustomViewBase<IMenuViewModel>
    {
        private Button _continueBtn;
        private Button _settingsBtn;
        private Button _menuBtn;

        protected override void InitializeView()
        {
            Debug.LogWarning("<color=red>init main menu view</color>");
            _continueBtn = ContentContainer.GetVisualElement<Button>(UIElementId.ContinueBtnId, name);
            _settingsBtn = ContentContainer.GetVisualElement<Button>(UIElementId.SettingsBtnId, name);
            _menuBtn = ContentContainer.GetVisualElement<Button>(UIElementId.MenuBtnId, name);
        }

        protected override void CreateAndInitComponents()
        {
            Debug.LogWarning("<color=red>create main menu view</color>");
        }

        protected override void Localize()
        {
            Debug.LogWarning("<color=red>localize main menu view</color>");
            ViewMainHeader.text = LocalizationManager.Localize(LocalizationNameID.Pause).ToUpper();
            _continueBtn.text = LocalizationManager.Localize(LocalizationNameID.Continue).ToUpper();
            _settingsBtn.text = LocalizationManager.Localize(LocalizationNameID.Settings).ToUpper();
            _menuBtn.text = LocalizationManager.Localize(LocalizationNameID.ToMainMenu).ToUpper();
        }

        protected override void InitializeCallbacks()
        {
            Debug.LogWarning("<color=red>init callbacks main menu view</color>");
            CallbacksCache.Add(_continueBtn, _ => ViewModel.ContinueBtnClicked());
            CallbacksCache.Add(_settingsBtn, _ => ViewModel.SettingsBtnClicked());
            CallbacksCache.Add(_menuBtn, _ => ViewModel.MenuBtnClicked());
        }
    }
}
