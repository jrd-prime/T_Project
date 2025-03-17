using Code.Core.Data;
using Code.Core.Providers.Localization;
using Code.Extensions;
using Code.UI._Base.View;
using R3;
using UnityEngine.UIElements;

namespace Code.UI.Pause.View
{
    public class PauseViewMain : CustomSubViewBase<IPauseViewModel>
    {
        private Button _continueBtn;
        private Button _menuBtn;
        private VisualElement _content;

        protected override void InitializeView()
        {
            _content = RootContainer.GetVisualElement<VisualElement>(UIElementId.PauseMainContainerId, name);

            _continueBtn = _content.GetVisualElement<Button>(UIElementId.ContinueBtnId, name);
            _menuBtn = _content.GetVisualElement<Button>(UIElementId.MenuBtnId, name);
        }

        protected override void CreateAndInitComponents()
        {
        }

        protected override void Localize()
        {
            ViewMainHeader.text = LocalizationManager.Localize(LocalizationNameID.Pause).ToUpper();
        }

        protected override void InitializeCallbacks()
        {
            CallbacksCache.TryAdd(_continueBtn, _ => ViewModel.ContinueButtonClicked.OnNext(Unit.Default));
            CallbacksCache.TryAdd(_menuBtn, _ => ViewModel.MenuButtonClicked.OnNext(Unit.Default));
        }
    }
}
