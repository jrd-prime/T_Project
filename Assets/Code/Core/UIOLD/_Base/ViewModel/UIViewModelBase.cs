using System;
using R3;

namespace Code.Core.UIOLD._Base.ViewModel
{
    public interface IUIViewModel
    {
    }

    public abstract class UIViewModelBase : IUIViewModel, IDisposable
    {
        protected readonly CompositeDisposable Disposables = new();

        public void Dispose()
        {
            Disposables?.Dispose();
        }
    }
}
