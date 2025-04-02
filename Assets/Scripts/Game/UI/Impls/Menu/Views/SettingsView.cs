using Core.Extensions;
using Core.Providers.Localization;
using Db.Data;
using Game.UI.Common;
using UnityEngine.UIElements;

namespace Game.UI.Impls.Menu.Views
{
    public class SettingsView : CustomViewBase<IMenuViewModel>
    {
        private Button _backBtn;
        private Button _audioBtn;
        private Label _soundBtnTtl;
        private VisualElement _soundBtnOnOff;
        private Button _videoBtn;

        protected override void InitializeView()
        {
            _videoBtn = ContentContainer.GetVisualElement<Button>(UIElementId.VideoBtnId, name);
            _audioBtn = ContentContainer.GetVisualElement<Button>(UIElementId.AudioBtnId, name);
            _backBtn = ContentContainer.GetVisualElement<Button>(UIElementId.BackBtnId, name);
        }

        protected override void CreateAndInitComponents()
        {
        }

        protected override void Localize()
        {
            ViewMainHeader.text = LocalizationManager.Localize(LocalizationNameID.Settings).ToUpper();
            _videoBtn.text = LocalizationManager.Localize(LocalizationNameID.Video).ToUpper();
            _audioBtn.text = LocalizationManager.Localize(LocalizationNameID.Audio).ToUpper();
            _backBtn.text = LocalizationManager.Localize(LocalizationNameID.Back).ToUpper();
        }

        protected override void InitializeCallbacks()
        {
            CallbacksCache.Add(_backBtn, _ => ViewModel.BackBtnClicked());
            CallbacksCache.Add(_audioBtn, _ => ViewModel.AudioBtnClicked());
            CallbacksCache.Add(_videoBtn, _ => ViewModel.VideoBtnClicked());
        }
    }
}
