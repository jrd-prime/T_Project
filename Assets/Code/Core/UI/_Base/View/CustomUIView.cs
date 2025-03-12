using Code.Core.UI._Base.ViewModel;
using Code.Extensions;
using VContainer;

namespace Code.Core.UI._Base.View
{
    public abstract class CustomUIView<TViewModel> : UIViewBase where TViewModel : class, IUIViewModel
    {
        protected TViewModel ViewModel { get; private set; }
        protected IObjectResolver Resolver { get; private set; }

        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            Resolver = resolver;
            ViewModel = Resolver.ResolveAndCheckOnNull<TViewModel>();
        }
    }
}
