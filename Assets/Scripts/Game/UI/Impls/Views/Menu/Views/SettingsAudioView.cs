using Game.Extensions;
using Game.UI.Common;
using Game.UI.Data;
using Infrastructure.Localization;
using UnityEngine.UIElements;

namespace Game.UI.Impls.Views.Menu.Views
{
    public class SettingsAudioView : CustomViewBase<IMenuViewModel>
    {
        private Button _backBtn;
        private Button _audioBtn;
        private Label _soundBtnTtl;
        private VisualElement _soundBtnOnOff;
        private Button _videoBtn;

        protected override void InitializeView()
        {
            _backBtn = ContentContainer.GetVisualElement<Button>(UIElementId.BackBtnId, name);
        }

        protected override void CreateAndInitComponents()
        {
        }

        protected override void Localize()
        {
            // TODO - this is a temporary solution, we need to refactor this
            {
                var mainHeader = LocalizationManager.Localize(LocalizationNameID.Settings);
                var currentViewHeader = LocalizationManager.Localize(LocalizationNameID.Audio);
                ViewMainHeader.text = (mainHeader + " > " + currentViewHeader).ToUpper();
            }
            _backBtn.text = LocalizationManager.Localize(LocalizationNameID.Back).ToUpper();
        }

        protected override void InitializeCallbacks()
        {
            CallbacksCache.Add(_backBtn, _ => ViewModel.BackBtnClicked());
        }
    }
}
