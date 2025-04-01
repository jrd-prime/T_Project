using System;
using Game.UI.Common.Base.Model;
using R3;
using Zenject;

namespace Game.UI.Common.Base.ViewModel
{
    public interface IUIViewModel : IInitializable, IDisposable
    {
    }

    public abstract class UIViewModelBase<TUIModel> : IUIViewModel
        where TUIModel : IUIModel
    {
        protected TUIModel Model { get; private set; }
        protected SignalBus SignalBus { get; private set; }

        [Inject]
        private void Construct(TUIModel model, SignalBus signalBus)
        {
            Model = model;
            SignalBus = signalBus;

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
