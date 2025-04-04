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
    public sealed class UIManager : MonoBehaviour, IUIManager
    {
        [SerializeField] private UIViewer viewer;
        [SerializeField] private ViewRegistryDataVo[] viewRegistryData = Array.Empty<ViewRegistryDataVo>();

        [Inject] private readonly SignalBus _signalBus;

        private readonly Dictionary<ViewRegistryType, IUIViewRegistry> _viewsRegistry = new();
        public Stack<(string viewId, UIViewer.Layer layer)> _viewStack { get; } = new();

        private ViewRegistryType _currentViewRegistryType = ViewRegistryType.NotSet;
        private IUIViewRegistry _currentViewRegistry;
        private string _currentViewId;
        private string _currentOverlayView;

        private void Awake()
        {
            if (!viewer) Log.Error("Viewer not set");
            if (viewRegistryData.Length == 0) Log.Error("Main views data not set");

            InitializeMainViews();

            _signalBus.Subscribe<ShowViewSignalVo>(OnShowViewSignal);
            _signalBus.Subscribe<SwitchToPreviousViewSignalVo>(OnSwitchToPreviousViewSignal);
        }

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

        public bool IsViewActive(string viewId) => _currentViewId == viewId;
        public bool IsOverlayViewActive() => _currentOverlayView != null;

        public void CloseOverlayView()
        {
            Log.Warn("Close overlay view");
            viewer.ClearLayer(UIViewer.Layer.Top);
            _currentOverlayView = null;
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
            Log.Warn($"// Show view -> {type} / {viewId} on layer {layer} (overlay: {isOverlay})");

            _currentViewRegistry = GetViewRegistry(type);

            if (!isOverlay) _currentViewId = viewId;

            if (_currentViewRegistryType == ViewRegistryType.NotSet)
            {
                Log.Warn("// registry type not set. push in stack");
                if (isOverlay) Log.Warn("isOverlay = true. It's not work when registryType != currentRegistryType");
                _currentViewRegistryType = type;
                _viewStack.Push((viewId, layer));
            }
            else if (_currentViewRegistryType == type)
            {
                Log.Warn("// same registry.");

                if (_viewStack.Count == 0) Log.Error("// Why stack is empty???");

                if (!isOverlay)
                {
                    
                    Log.Warn("// overlay = FALSE");
                    if (_viewStack.Peek().viewId != viewId)
                    {
                        viewer.ClearLayer(layer);
                        Log.Warn("// push view to stack -> " + viewId + " on layer " + layer);
                        _viewStack.Push((viewId, layer));
                    }
                }
                else
                {
                    Log.Warn("// overlay = TRUE");
                    _currentOverlayView = viewId;
                }
            }
            else if (_currentViewRegistryType != type)
            {
                Log.Warn("// new registry. hide views. clear stack. push in stack");
                if (isOverlay) Log.Warn("isOverlay = true. It's not work when registryType != currentRegistryType");
                _currentViewRegistryType = type;
                viewer.HideView();
                _viewStack.Clear();
                _viewStack.Push((viewId, layer));
                _currentOverlayView = null;
            }

            var templateData = new ViewTemplateData
            {
                ViewId = viewId,
                StateId = type.ToString(),
                Template = _currentViewRegistry.GetView(viewId),
                InSafeZone = false,
                DebugData = new DebugDataVo("stack", _viewStack.Count, isOverlay)
            };

            viewer.ShowView(templateData, layer);
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
        }

        private IUIViewRegistry GetViewRegistry(ViewRegistryType type)
        {
            if (_viewsRegistry.TryGetValue(type, out var viewRegistry)) return viewRegistry;

            throw new KeyNotFoundException($"View registry for type {type} not found in cache");
        }

        public bool IsMainViewActive(ViewRegistryType type)
        {
            Log.Warn("Is main view active " + type + " / " + _currentViewRegistryType + " / " + _currentViewId);
            return _currentViewRegistryType == type && _currentViewId == ViewIDConst.Main;
        }


        private void InitializeMainViews()
        {
            foreach (var viewDataVo in viewRegistryData)
            {
                if (!viewDataVo.viewRegistry)
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

    public record DebugDataVo(string Name, int ViewStackCount, bool IsOverlay)
    {
        public string Name { get; } = Name;
        public int ViewStackCount { get; } = ViewStackCount;
        public bool IsOverlay { get; } = IsOverlay;
    }
}
