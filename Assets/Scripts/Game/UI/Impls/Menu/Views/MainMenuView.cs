using Core.Extensions;
using Core.Providers.Localization;
using Db.Data;
using Game.UI.Common;
using ModestTree;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UI.Impls.Menu.Views
{
    public sealed class MainMenuView : CustomViewBase<IMenuViewModel>
    {
        private Button _playBtn;
        private Button _settingsBtn;
        private Button _exitBtn;

        protected override void InitializeView()
        {
            _playBtn = ContentContainer.GetVisualElement<Button>(UIElementId.StartBtnId, name);
            _settingsBtn = ContentContainer.GetVisualElement<Button>(UIElementId.SettingsBtnId, name);
            _exitBtn = ContentContainer.GetVisualElement<Button>(UIElementId.ExitBtnId, name);
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
            CallbacksCache.Add(_playBtn, _ => ViewModel.StartBtnClicked());
            CallbacksCache.Add(_settingsBtn, _ => ViewModel.SettingsBtnClicked());
            CallbacksCache.Add(_exitBtn, _ => ViewModel.ExitBtnClicked());
        }
    }
}
