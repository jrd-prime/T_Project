using System;
using System.Collections.Generic;
using System.Linq;
using Core.Managers.UI.Interfaces;
using Core.Managers.UI.Signals;
using Game.UI.Common;
using Game.UI.Common.Base.Data;
using Game.UI.Data;
using Game.UI.Interfaces;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Core.Managers.UI.Impls
{
    //TODO баг, когда пауза открывается, то в стек добавляется и мэйн, хотя его не надо добавлять, эскейп должен работать не так
    public sealed class UIManager : MonoBehaviour, IUIManager
    {
        [SerializeField] private UIViewer viewer;
        [SerializeField] private ViewRegistryDataVo[] viewRegistryData = Array.Empty<ViewRegistryDataVo>();

        [Inject] private readonly SignalBus _signalBus;

        private readonly Dictionary<ViewRegistryType, IUIViewRegistry> _viewsRegistry = new();
        public Stack<(string viewId, UIViewer.Layer layer)> _viewStack { get; } = new();


        public void ShowPreviousViewNew()
        {
            Log.Warn("show previous view new");
            if (_viewStack.Count <= 1) return;
            var current = _viewStack.Pop();
            viewer.ClearLayer(current.layer);

            if (_viewStack.Count <= 0) return;

            var previous = _viewStack.Peek();
            Log.Warn($"Restoring previous view: {previous.viewId} on layer {previous.layer}");
            ShowViewNew(_currentViewRegistryType, previous.viewId, previous.layer);
        }

        private ViewRegistryType _currentViewRegistryType = ViewRegistryType.NotSet;
        private IUIViewRegistry _currentViewRegistry;
        private string _currentViewId;

        private void Awake()
        {
            if (!viewer) Log.Error("Viewer not set");
            if (viewRegistryData.Length == 0) Log.Error("Main views data not set");

            InitializeMainViews();

            _signalBus.Subscribe<ShowViewSignalVo>(OnShowViewSignal);
            _signalBus.Subscribe<SwitchToPreviousViewSignalVo>(OnSwitchToPreviousViewSignal);
        }

        private void OnSwitchToPreviousViewSignal() => ShowPreviousViewNew();

        private void OnShowViewSignal(ShowViewSignalVo signal)
        {
            Log.Warn("switch local view signal -> " + signal.ViewRegistryType + " / " + signal.ViewId);
            ShowViewNew(signal.ViewRegistryType, signal.ViewId, signal.Layer, signal.IsOverlay);
        }

        public void ShowViewNew(ViewRegistryType type, string viewId, UIViewer.Layer layer = UIViewer.Layer.Default,
            bool isOverlay = false)
        {
            Log.Warn($"Show view -> {type} / {viewId} on layer {layer} (overlay: {isOverlay})");

            if (_currentViewRegistryType == ViewRegistryType.NotSet) _currentViewRegistryType = type;

            if (!_viewsRegistry.TryGetValue(type, out var viewRegistry))
            {
                Log.Error($"view registry for type {type} not found");
                return;
            }

            if (_currentViewRegistryType == type)
            {
                Log.Warn("push view to stack -> " + viewId + " on layer " + layer);

                if (_viewStack.Count == 0) _viewStack.Push((viewId, layer));

                if (_viewStack.Count > 0)
                {
                    if (_viewStack.Peek().viewId != viewId) _viewStack.Push((viewId, layer));
                }
            }
            else
            {
                Log.Warn("clear view stack");
                _viewStack.Clear();
            }


            var view = viewRegistry.GetView(viewId);
            var templateData = new ViewTemplateData
            {
                ViewId = viewId, StateId = type.ToString(), Template = view, InSafeZone = false,
                DebugData = new DebugDataVo("stack", _viewStack.Count)
            };

            viewer.ShowView(templateData, layer);

            _currentViewRegistryType = type;
            _currentViewId = viewId;
        }

        public void ShowView1(ViewRegistryType type, string viewId)
        {
            throw new NotImplementedException();
        }


        public void ShowView(ViewRegistryType type, string viewId)
        {
            ShowView1(type, viewId, UIViewer.Layer.Default, replace: true);
        }

        public void ShowView1(ViewRegistryType type, string viewId, UIViewer.Layer layer, bool replace = false,
            bool isOverlay = false)
        {
            Log.Info($"show view -> {type} / {viewId} on layer {layer} (replace: {replace}, overlay: {isOverlay})");

            if (!_viewsRegistry.TryGetValue(type, out var viewRegistry))
            {
                Log.Error($"view registry for type {type} not found");
                return;
            }

            var view = viewRegistry.GetView(viewId);
            var templateData = new ViewTemplateData
            {
                ViewId = viewId, StateId = type.ToString(), Template = view, InSafeZone = false
            };

            if (replace && layer == UIViewer.Layer.Default) viewer.ClearLayer(UIViewer.Layer.Default);

            // Добавляем в стек только базовые вьюшки (не оверлеи)
            if (!isOverlay && (_viewStack.Count == 0 || _viewStack.Peek().viewId != viewId))
            {
                _viewStack.Push((viewId, layer));
            }

            Log.Info($"Stack after show: {string.Join(", ", _viewStack.Select(v => v.viewId))}");
        }

        public void SetAndShowBaseView(ViewRegistryType type, string viewId = ViewIDConst.Main)
        {
            Log.Info($"set base view {type} with viewId {viewId}");
            HideAllViews();
            _currentViewRegistryType = type;
            _currentViewRegistry = _viewsRegistry[type];
            ShowView1(type, viewId, UIViewer.Layer.Default, true);
        }

        public void HideView(string viewId)
        {
            Log.Info($"hide view {viewId}");
            var viewInStack = _viewStack.FirstOrDefault(v => v.viewId == viewId);
            if (viewInStack.viewId == null) return;

            var tempStack = new Stack<(string viewId, UIViewer.Layer layer)>();
            while (_viewStack.Count > 0)
            {
                var item = _viewStack.Pop();
                if (item.viewId != viewId)
                    tempStack.Push(item);
            }

            while (tempStack.Count > 0) _viewStack.Push(tempStack.Pop());

            viewer.ClearLayer(viewInStack.layer);
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

        public bool IsGameplayMainViewActive()
        {
            var d = _currentViewRegistryType == ViewRegistryType.Gameplay && _viewStack.Count == 1;
            Debug.LogWarning("d = " + d);
            return d;
        }

        public bool IsMainViewActive(ViewRegistryType type) =>
            _currentViewRegistryType == type && _currentViewId == ViewIDConst.Main;


        private void InitializeMainViews()
        {
            foreach (var viewDataVo in viewRegistryData)
            {
                if (viewDataVo.viewRegistry == null)
                {
                    Log.Error("View registry not set for " + viewDataVo.type);
                    continue;
                }

                RegisterView(viewDataVo.type, viewDataVo.viewRegistry);
            }

            Log.Info("initialized global views: " + _viewsRegistry.Count);
        }

        private void RegisterView(ViewRegistryType viewId, IUIViewRegistry view) => _viewsRegistry.TryAdd(viewId, view);
    }

    public record DebugDataVo(string Name, int ViewStackCount)
    {
        public string Name { get; } = Name;
        public int ViewStackCount { get; } = ViewStackCount;
    }
}
