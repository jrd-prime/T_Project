﻿using Code.Core.Data;
using Code.Core.Providers.Localization;
using Code.Extensions;
using Code.UI._Base.View;
using R3;
using UnityEngine.UIElements;

namespace Code.UI.Menu.View
{
    public class MenuViewSettings : CustomSubViewBase<IMenuViewModel>
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
            ViewMainHeader.text = LocalizationManager.Localize(LocalizationNameID.Settings).ToUpper();
            _videoBtn.text = LocalizationManager.Localize(LocalizationNameID.Video).ToUpper();
            _audioBtn.text = LocalizationManager.Localize(LocalizationNameID.Audio).ToUpper();
        }

        protected override void InitializeCallbacks()
        {
            CallbacksCache.Add(_backBtn, _ => ViewModel.BackButtonClicked.OnNext(Unit.Default));
            CallbacksCache.Add(_audioBtn, _ => ViewModel.AudioButtonClicked.OnNext(Unit.Default));
            CallbacksCache.Add(_videoBtn, _ => ViewModel.VideoButtonClicked.OnNext(Unit.Default));
        }
    }
}
