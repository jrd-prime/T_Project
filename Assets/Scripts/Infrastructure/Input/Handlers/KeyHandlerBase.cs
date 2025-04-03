using System;
using System.Collections.Generic;
using Core.Managers.HSM.Impls;
using Core.Managers.UI.Interfaces;
using Infrastructure.Input.Interfaces;
using Zenject;

namespace Infrastructure.Input.Handlers
{
    public abstract class KeyHandlerBase<TSignal> : IDisposable where TSignal : IKeySignal
    {
        protected readonly SignalBus SignalBus;
        protected readonly IUIManager UIManager;
        protected readonly HSM HSM;

        private readonly Dictionary<Type, Action<TSignal>> _subscriptions = new();

        protected KeyHandlerBase(SignalBus signalBus, IUIManager uiManager, HSM hsm)
        {
            SignalBus = signalBus;
            UIManager = uiManager;
            HSM = hsm;
            InitHandler();
        }

        private void InitHandler() => InitializeSubscriptions();

        protected void AddSubscription(Action<TSignal> onEscapeSignal)
        {
            SignalBus.Subscribe<TSignal>(onEscapeSignal);
            _subscriptions.Add(typeof(TSignal), onEscapeSignal);
        }

        protected abstract void InitializeSubscriptions();

        private void Unsubscribe()
        {
            foreach (var subscription in _subscriptions)
                SignalBus.Unsubscribe<TSignal>(subscription.Value);

            _subscriptions.Clear();
        }

        public void Dispose() => Unsubscribe();
    }
}
