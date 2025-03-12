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

        protected override void InitializeVisualElements()
        {
            Debug.LogWarning("InitializeVisualElements");

            _startBtn = Root.GetVisualElement<Button>("start-btn", name);
        }

        protected override void UnsubscribeFromEvents()
        {
            _startBtn.UnregisterCallback<ClickEvent>(evt =>
            {
                Debug.Log("ClickEvent triggered");
                ViewModel.StartButtonClickCommand.Execute(Unit.Default);
            });
        }

        protected override void SubscribeToEvents()
        {
            Debug.LogWarning("SubscribeToEvents");

            if (_startBtn == null)
            {
                Debug.LogError("Start button is null in OnEnable!");
                return;
            }

            _startBtn.RegisterCallback<ClickEvent>(evt =>
            {
                Debug.Log("ClickEvent triggered");
                ViewModel.StartButtonClickCommand.Execute(Unit.Default);
            });
        }


        public override void Show()
        {
            Debug.LogWarning("show menu view");
            Root.style.display = DisplayStyle.Flex;
        }

        public override void Hide()
        {
            Debug.LogWarning("hide menu view");
            Root.style.display = DisplayStyle.None;
        }
    }
}
