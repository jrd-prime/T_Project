using Code.Core.WORK.UI.Base.ViewModel;
using R3;
using UnityEngine.UIElements;

namespace Code.Core.WORK.UI.Base.Component
{
    public abstract class SubViewComponentBase<TViewModel> where TViewModel : IUIViewModel
    {
        protected readonly VisualElement Root;
        protected readonly TViewModel ViewModel;
        protected readonly CompositeDisposable Disposables;

        protected SubViewComponentBase(TViewModel viewModel, in VisualElement root,
            in CompositeDisposable disposables)
        {
            Root = root;
            ViewModel = viewModel;
            Disposables = disposables;

            InitComponent();
        }

        private void InitComponent()
        {
            InitElements();
            Init();
        }

        protected abstract void Init();

        protected abstract void InitElements();
    }
}
