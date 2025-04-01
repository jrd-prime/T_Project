using System;
using System.Collections.Generic;
using System.Linq;
using Core.Managers.UI.Interfaces;
using Core.Managers.UI.Signals;
using Game.UI.Common.Base.Data;
using Game.UI.Data;
using Game.UI.Interfaces;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Core.Managers.UI.Impls
{
    public sealed class UIManager : MonoBehaviour, IUIManager
    {
        [SerializeField] private UIViewer viewer;
        [SerializeField] private ViewRegistryDataVo[] viewRegistryData = Array.Empty<ViewRegistryDataVo>();

        private readonly Dictionary<ViewRegistryType, IUIViewRegistry> _viewsRegistry = new();
        private readonly Stack<(string viewId, UIViewer.Layer layer)> _viewStack = new();
        private ViewRegistryType _currentViewRegistryType;
        private IUIViewRegistry _currentViewRegistry;

        [Inject] private readonly SignalBus _signalBus;


        private void Awake()
        {
            if (!viewer) Log.Error("Viewer not set");
            if (viewRegistryData.Length == 0) Log.Error("Main views data not set");

            InitializeMainViews();

            _signalBus.Subscribe<SwitchLocalViewSignalVo>(OnSwitchLocalViewSignal);
        }

        private void OnSwitchLocalViewSignal(SwitchLocalViewSignalVo signal) => SwitchToView(signal.ViewId);

        public void ShowView(ViewRegistryType type, string viewId)
        {
            ShowView(type, viewId, UIViewer.Layer.Back, replace: true);
        }

        public void ShowView(ViewRegistryType type, string viewId, UIViewer.Layer layer, bool replace = false)
        {
            Log.Info($"show view -> {type} / {viewId} on layer {layer} (replace: {replace})");
            if (!_viewsRegistry.TryGetValue(type, out var viewRegistry))
            {
                Log.Error($"view registry for type {type} not found");
                return;
            }

            var view = viewRegistry.GetView(viewId);
            var templateData = new SubViewTemplateData { Template = view, InSafeZone = false };

            if (replace && layer == UIViewer.Layer.Back)
            {
                viewer.ClearLayer(UIViewer.Layer.Back);
            }

            switch (layer)
            {
                case UIViewer.Layer.Back:
                    viewer.ShowUnderSubView(templateData);
                    break;
                case UIViewer.Layer.Main:
                    viewer.ShowNewBase(templateData);
                    break;
                case UIViewer.Layer.Top:
                    viewer.ShowOverSubView(templateData);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(layer), layer, null);
            }

            if (_viewStack.Count == 0 || _viewStack.Peek().viewId != viewId)
                _viewStack.Push((viewId, layer));
        }

        public void HideView(string viewId)
        {
            Log.Info($"hide view {viewId}");
            var viewInStack = _viewStack.FirstOrDefault(v => v.viewId == viewId);
            if (viewInStack.viewId == null) return;

            // Удаляем все вхождения viewId из стека
            var tempStack = new Stack<(string viewId, UIViewer.Layer layer)>();
            while (_viewStack.Count > 0)
            {
                var item = _viewStack.Pop();
                if (item.viewId != viewId)
                    tempStack.Push(item);
            }

            while (tempStack.Count > 0)
            {
                _viewStack.Push(tempStack.Pop());
            }

            viewer.ClearLayer(viewInStack.layer); // Удаляем только указанный слой
        }

        public void HideAllViews()
        {
            Log.Info("hide all views");
            viewer.HideView();
            _viewStack.Clear();
            // Не сбрасываем _currentViewRegistryType, если это не требуется
            // Если нужно сбросить, можно сделать _currentViewRegistryType = default(ViewRegistryType);
        }

        public void SwitchToView(string viewId)
        {
            Log.Info($"switch to view {viewId}");
            if (_viewStack.Count > 0 && _viewStack.Peek().viewId != viewId)
                HideView(_viewStack.Peek().viewId);

            if (!_currentViewRegistry.HasView(viewId))
                Log.Error($"Registry {_currentViewRegistry.GetType().Name} doesn't have view with id: {viewId}");

            ShowView(_currentViewRegistryType, viewId);
        }

        public void ShowPreviousView()
        {
            Log.Info("show previous view");
            if (_viewStack.Count <= 1) return;
            var current = _viewStack.Pop();
            viewer.ClearLayer(current.layer);

            if (_viewStack.Count > 0)
            {
                var previous = _viewStack.Peek();
                ShowView(_currentViewRegistryType, previous.viewId, previous.layer, replace: false);
            }
        }

        public void SetAndShowBaseView(ViewRegistryType type)
        {
            Log.Info($"set base view {type}");
            HideAllViews();
            _currentViewRegistryType = type;
            _currentViewRegistry = _viewsRegistry[type];
            ShowView(type, "main", UIViewer.Layer.Back, replace: true);
        }

        private void InitializeMainViews()
        {
            Log.Info("initialize main views = " + viewRegistryData.Length);
            foreach (var viewDataVo in viewRegistryData) RegisterView(viewDataVo.type, viewDataVo.aViewRegistry);
        }

        private void RegisterView(ViewRegistryType viewId, IUIViewRegistry view) => _viewsRegistry.TryAdd(viewId, view);
    }
}
