using Code.Core.WORK.UI.Base.View;
using R3;
using UnityEngine.UIElements;

namespace Code.Core.WORK.UIStates.Menu.View
{
    public class MenuViewSettings : CustomSubViewBase<IMenuViewModel>
    {
        private Button _backBtn;
        private VisualElement _content;
        private Button _soundBtn;
        private Label _soundBtnTtl;
        private VisualElement _soundBtnOnOff;
        private Label _header;

        protected override void InitializeView()
        {
            // _content = ContentContainer.Q<VisualElement>(UIElementId.MenuContentId).CheckOnNull();
            // _backBtn = _content.Q<Button>(UIElementId.BackBtnId).CheckOnNull();
            // _soundBtn = _content.Q<Button>(UIElementId.SoundBtnId).CheckOnNull();
            // _soundBtnTtl = _soundBtn.Q<Label>("sound-btn-ttl").CheckOnNull();
            // _soundBtnOnOff = _soundBtn.Q<VisualElement>("on-off").CheckOnNull();
            // _soundBtnOnOff.Q<VisualElement>("on").CheckOnNull().style.display = DisplayStyle.Flex;
            // _soundBtnOnOff.Q<VisualElement>("off").CheckOnNull().style.display = DisplayStyle.None;

            // _header = ContentContainer.Q<Label>(UIElementId.MenuHeaderId).CheckOnNull();
        }

        protected override void CreateAndInitComponents()
        {
        }

        protected override void Localize()
        {
            // _backBtn.text = LocalizationManager.GetString(TextNameId.backBtnNameId).ToUpper();
            // _header.text = LocalizationManager.GetString(TextNameId.settingsBtnNameId);
        }

        protected override void InitializeCallbacks()
        {
            CallbacksCache.Add(_backBtn, _ => ViewModel.BackButtonClicked.OnNext(Unit.Default));
        }
    }
}
