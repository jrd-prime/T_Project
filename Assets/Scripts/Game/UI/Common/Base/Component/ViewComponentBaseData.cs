using Game.UI.Common.Base.ViewModel;
using UnityEngine.UIElements;

namespace Game.UI.Common.Base.Component
{
    public class ViewComponentBaseData<TUIViewModel> where TUIViewModel : IUIViewModel
    {
        public readonly VisualElement Root;
        public readonly TUIViewModel ViewModel;

        public ViewComponentBaseData(TUIViewModel viewModel, VisualElement contentContainer)
        {
            Root = contentContainer;
            ViewModel = viewModel;
        }
    }
}
