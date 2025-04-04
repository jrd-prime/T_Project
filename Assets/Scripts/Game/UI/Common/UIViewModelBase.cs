﻿using System;
using Core.Managers.HSM.Signals;
using Core.Managers.UI.Impls;
using Core.Managers.UI.Signals;
using Game.UI.Data;
using Game.UI.Interfaces.Model;
using R3;
using Zenject;

namespace Game.UI.Common
{
    public abstract class UIViewModelBase<TUIModel> : IUIViewModel where TUIModel : IUIModel
    {
        protected TUIModel Model { get; private set; }
        protected SignalBus SignalBus { get; private set; }
        protected ViewRegistryType RegistryType { get; private set; }
        protected readonly CompositeDisposable Disposables = new();


        [Inject]
        private void Construct(TUIModel model, SignalBus signalBus)
        {
            Model = model;
            SignalBus = signalBus;

            if (Model == null) throw new NullReferenceException($"{typeof(TUIModel)} is null");

            RegistryType = GetRegistryType();
        }


        protected void ShowLocalView(string viewId, UIRenderer.Layer layer = UIRenderer.Layer.Default,
            bool isOverlay = false) =>
            SignalBus.Fire(new ShowViewSignalVo(RegistryType, viewId, layer, isOverlay));

        protected void SwitchToPreviousView() => SignalBus.Fire(new ShowPreviousViewSignalVo());
        protected void ChangeGameStateTo(Type type) => SignalBus.Fire(new ChangeGameStateSignalVo(type));

        protected void ChangeGameStateAndShowView(Type stateType, ViewRegistryType registryType, string viewId)
        {
            SignalBus.Fire(new ChangeGameStateSignalVo(stateType));
            SignalBus.Fire(new ShowViewSignalVo(registryType, viewId));
        }

        public abstract void Initialize();
        protected abstract ViewRegistryType GetRegistryType();

        public void Dispose() => Disposables?.Dispose();
    }
}
