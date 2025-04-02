using Core.Extensions;
using Core.Providers.Localization;
using Db.Data;
using Game.UI.Common;
using UnityEngine.UIElements;

namespace Game.UI.Impls.Menu.Views
{
    public class SettingsVfxView : CustomViewBase<IMenuViewModel>
    {
        private Button _backBtn;
        private VisualElement _content;
        private Button _audioBtn;
        private Label _soundBtnTtl;
        private VisualElement _soundBtnOnOff;
        private Button _videoBtn;

        protected override void InitializeView()
        {
            _content = RootContainer.GetVisualElement<VisualElement>(UIElementId.MenuSettingsContainerId, name);

            _videoBtn = _content.GetVisualElement<Button>(UIElementId.VideoBtnId, name);
            _audioBtn = _content.GetVisualElement<Button>(UIElementId.AudioBtnId, name);
            _backBtn = _content.GetVisualElement<Button>(UIElementId.BackBtnId, name);
        }

        protected override void CreateAndInitComponents()
        {
        }

        protected override void Localize()
        {
            // TODO - this is a temporary solution, we need to refactor this
            {
                var mainHeader = LocalizationManager.Localize(LocalizationNameID.Settings);
                var currentViewHeader = LocalizationManager.Localize(LocalizationNameID.Video);
                ViewMainHeader.text = (mainHeader + " > " + currentViewHeader).ToUpper();
            }

            _videoBtn.text = LocalizationManager.Localize(LocalizationNameID.Video).ToUpper();
            _audioBtn.text = LocalizationManager.Localize(LocalizationNameID.Audio).ToUpper();
        }

        protected override void InitializeCallbacks()
        {
            CallbacksCache.Add(_backBtn, _ => ViewModel.BackBtnClicked());
            CallbacksCache.Add(_audioBtn, _ => ViewModel.AudioBtnClicked());
            CallbacksCache.Add(_videoBtn, _ => ViewModel.VideoBtnClicked());
        }
    }
}
