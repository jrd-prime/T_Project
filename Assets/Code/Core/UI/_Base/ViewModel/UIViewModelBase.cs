using System;
using Code.Core.UI._Base.Model;
using R3;
using VContainer;
using VContainer.Unity;

namespace Code.Core.UI._Base.ViewModel
{
    public interface IUIViewModel : IInitializable
    {
    }

    public abstract class UIViewModelBase<TUIModel, TSubStateEnum> : IUIViewModel, IInitializable, IDisposable
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
