using Code.Core.UIOLD._Base.Model;
using VContainer;

namespace Code.Core.UIOLD._Base.ViewModel
{
    public abstract class CustomUIViewModel<TModel> : UIViewModelBase where TModel : class, IUIModel
    {
        protected TModel Model { get; private set; }

        [Inject]
        private void Construct(TModel model)
        {
            Model = model;
        }
    }
}
