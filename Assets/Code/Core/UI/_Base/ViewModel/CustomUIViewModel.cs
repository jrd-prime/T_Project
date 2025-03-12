using Code.Core.UI._Base.Model;
using VContainer;

namespace Code.Core.UI._Base.ViewModel
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
