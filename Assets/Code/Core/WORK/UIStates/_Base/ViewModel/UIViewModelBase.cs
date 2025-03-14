using System;
using Code.Core.WORK.UIStates._Base.Model;
using R3;
using VContainer;
using VContainer.Unity;

namespace Code.Core.WORK.UIStates._Base.ViewModel
{
    public interface IUIViewModel : IInitializable
    {
    }

    public abstract class UIViewModelBase<TModel, TSubStateEnum> : IUIViewModel, IInitializable, IDisposable
        where TSubStateEnum : Enum
        where TModel : IUIModel<TSubStateEnum>
    {
        protected TModel Model { get; private set; }

        [Inject]
        private void Construct(TModel model)
        {
            Model = model;

            if (Model == null) throw new NullReferenceException($"{typeof(TModel)} is null");
        }

        protected readonly CompositeDisposable Disposables = new();

        public abstract void Initialize();

        public void Dispose()
        {
            Disposables?.Dispose();
        }
    }
}
