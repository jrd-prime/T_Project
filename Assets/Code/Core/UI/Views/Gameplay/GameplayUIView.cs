using Code.Core.UI._Base.View;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.Core.UI.Views.Gameplay
{
    public sealed class GameplayUIView : CustomUIView<GameplayUIViewModel>
    {
        protected override void UnsubscribeFromEvents()
        {
        }

        protected override void SubscribeToEvents()
        {
        }

        protected override void InitializeVisualElements()
        {
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
