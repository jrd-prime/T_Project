using System;
using Code.UI._Base.Model;
using R3;
using Zenject;

namespace Code.UI._Base.ViewModel
{
    public interface IUIViewModel : IInitializable, IDisposable
    {
    }

    public abstract class UIViewModelBase<TUIModel, TSubStateEnum> : IUIViewModel
        where TSubStateEnum : Enum
        where TUIModel : IUIModel<TSubStateEnum>
    {
        protected TUIModel Model { get; private set; }

        [Inject]
        private void Construct(TUIModel model)
        {
            Model = model;

            if (Model == null) throw new NullReferenceException($"{typeof(TUIModel)} is null");
        }

        protected readonly CompositeDisposable Disposables = new();

        public abstract void Initialize();

        public void Dispose()
        {
            Disposables?.Dispose();
        }
    }
}
