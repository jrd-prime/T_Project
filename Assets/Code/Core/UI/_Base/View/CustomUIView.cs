using Code.Core.UI._Base.ViewModel;
using Code.Extensions;
using UnityEngine;
using VContainer;

namespace Code.Core.UI._Base.View
{
    public abstract class CustomUIView<T> : UIViewBase where T : class, IUIViewModel
    {
        protected T Model { get; private set; }
        protected IObjectResolver Resolver { get; private set; }

        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            Debug.LogWarning("construct ");
            Resolver = resolver;
            Model = Resolver.ResolveAndCheckOnNull<T>();
        }
    }
}
