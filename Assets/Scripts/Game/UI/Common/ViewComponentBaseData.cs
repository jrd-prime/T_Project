using Game.UI.Interfaces.Model;
using UnityEngine.UIElements;

namespace Game.UI.Common
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
