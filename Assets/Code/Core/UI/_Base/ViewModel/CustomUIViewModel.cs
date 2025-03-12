using Code.Core.UI._Base.Model;

namespace Code.Core.UI._Base.ViewModel
{
    public abstract class CustomUIViewModel<TModel> : UIViewModelBase where TModel : class, IUIModel
    {
    }
}
