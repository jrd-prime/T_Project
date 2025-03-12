using System;
using R3;
using UnityEngine;

namespace Code.Core.UI._Base.ViewModel
{
    public interface IUIViewModel
    {
    }

    public abstract class UIViewModelBase : IUIViewModel, IDisposable
    {
        protected readonly CompositeDisposable Disposables = new();

        public void Dispose()
        {
            Debug.LogWarning("dispose");
            Disposables?.Dispose();
        }
    }
}
