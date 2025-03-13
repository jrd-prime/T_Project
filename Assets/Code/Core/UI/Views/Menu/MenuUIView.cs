using Code.Core.Providers.Localization;
using Code.Core.UI._Base.View;
using Code.Extensions;
using R3;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.Core.UI.Views.Menu
{
    internal sealed class MenuUIView : CustomUIView<MenuUIViewModel>
    {
        private Button _startBtn;

        protected override void InitializeViewElements()
        {
            _startBtn = Root.GetVisualElement<Button>("start-btn", name);
        }

        protected override void CreateAndInitComponents()
        {
        }

        protected override void Localize()
        {
            _startBtn.text = LocalizationProvider.Localize(LocalizationNameID.startBtnNameId).ToUpper();
        }

        protected override void InitializeCallbacks()
        {
            CallbacksCache.Add(_startBtn, _ =>
            {
                Debug.LogWarning("CLICKED");
                ViewModel.StartButtonClickCommand.Execute(Unit.Default);
            });
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }
    }
}
