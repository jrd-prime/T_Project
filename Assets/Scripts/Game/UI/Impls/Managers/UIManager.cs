﻿using System;
using System.Collections.Generic;
using Core.Managers.UI.Data;
using Core.Managers.UI.Interfaces;
using Game.UI.Data;
using Game.UI.Interfaces;
using Game.UI.Signals;
using ModestTree;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.UI.Impls.Managers
{
    public sealed class UIManager : MonoBehaviour, IUIManager
    {
        [SerializeField] private bool debug = true;
        [FormerlySerializedAs("uiRenderer")] [SerializeField] private UIViewer uiViewer;
        [SerializeField] private ViewRegistryDataVo[] viewRegistryData = Array.Empty<ViewRegistryDataVo>();

        [Inject] private readonly SignalBus _signalBus;
        private readonly Stack<(string viewId, ViewerLayer layer)> _viewStack = new();
        private readonly Dictionary<ViewRegistryType, IUIViewRegistry> _viewsRegistry = new();

        private ViewRegistryType _currentViewRegistryType = ViewRegistryType.NotSet;
        private IUIViewRegistry _currentViewRegistry;
        private string _currentViewId;
        private string _currentOverlayView;

        private void Awake()
        {
            if (!uiViewer) Log.Error("Viewer not set");
            if (viewRegistryData.Length == 0) Log.Error("Main views data not set");

            InitializeMainViews();

            _signalBus.Subscribe<ShowViewSignalVo>(OnShowViewSignal);
            _signalBus.Subscribe<ShowPreviousViewSignalVo>(OnSwitchToPreviousViewSignal);
        }

        public void ShowPreviousViewNew()
        {
            if (_viewStack.Count <= 1) return;
            var current = _viewStack.Pop();
            uiViewer.ClearLayer(current.layer);

            if (_viewStack.Count <= 0) return;

            var previous = _viewStack.Peek();
            if (debug) Log.Info($"Restoring previous view: {previous.viewId} on layer {previous.layer}");
            ShowView(new UIManagerViewDataVo(_currentViewRegistryType, previous.viewId, previous.layer));
        }

        public bool IsViewActive(string viewId) => _currentViewId == viewId;
        public bool IsOverlayViewActive() => _currentOverlayView != null;

        public void CloseOverlayView()
        {
            if (debug) Log.Warn("Close overlay view");
            uiViewer.ClearLayer(ViewerLayer.Top);
            _currentOverlayView = null;
        }

        public bool IsOverlayIt(string viewId) => _currentOverlayView == viewId;


        public void ShowView(UIManagerViewDataVo data)
        {
            _currentViewRegistry = GetViewRegistry(data.RegistryType);

            if (!data.IsOverlay) _currentViewId = data.ViewId;

            UpdateViewState(data);
            ShowViewWithTemplate(data);
        }

        /// <summary>
        /// Активен ли мэйн вьюв для типа вью регистра
        /// </summary>
        public bool IsMainViewActive(ViewRegistryType registryType) =>
            _currentViewRegistryType == registryType && _currentViewId == ViewIDConst.Main;

        private void UpdateViewState(UIManagerViewDataVo data)
        {
            if (_currentViewRegistryType == ViewRegistryType.NotSet)
            {
                InitializeViewStack(data);
                return;
            }

            if (_currentViewRegistryType == data.RegistryType)
            {
                HandleSameRegistry(data);
                return;
            }

            SwitchToNewRegistry(data);
        }

        private void InitializeViewStack(UIManagerViewDataVo data)
        {
            _currentViewRegistryType = data.RegistryType;
            _viewStack.Push((data.ViewId, data.ViewerLayer));
            // if (data.IsOverlay) Log.Warn("Overlay ignored: registry type not set");
        }

        private void HandleSameRegistry(UIManagerViewDataVo data)
        {
            if (_viewStack.Count == 0)
            {
                Log.Error("View stack is empty");
                return;
            }

            if (data.IsOverlay)
            {
                _currentOverlayView = data.ViewId;
                return;
            }

            if (_viewStack.Peek().viewId == data.ViewId) return;

            uiViewer.ClearLayer(data.ViewerLayer);
            _viewStack.Push((data.ViewId, data.ViewerLayer));
        }

        private void ShowViewWithTemplate(UIManagerViewDataVo data)
        {
            var templateData = new ViewTemplateData
            {
                ViewId = data.ViewId,
                StateId = data.RegistryType.ToString(),
                Template = _currentViewRegistry.GetView(data.ViewId),
                InSafeZone = false,
                UIViewerDebugData = new UIViewerDebugDataVo("stack", _viewStack.Count, data.IsOverlay)
            };

            uiViewer.ShowView(templateData, data.ViewerLayer);
        }

        private void SwitchToNewRegistry(UIManagerViewDataVo data)
        {
            _currentViewRegistryType = data.RegistryType;
            uiViewer.HideView();
            _viewStack.Clear();
            _viewStack.Push((data.ViewId, data.ViewerLayer));
            _currentOverlayView = null;
            // if (data.IsOverlay) Log.Warn("Overlay ignored: different registry type");
        }

        private IUIViewRegistry GetViewRegistry(ViewRegistryType registryType)
        {
            if (_viewsRegistry.TryGetValue(registryType, out var viewRegistry)) return viewRegistry;

            throw new KeyNotFoundException($"View registry for type {registryType} not found in cache");
        }

        private void OnSwitchToPreviousViewSignal() => ShowPreviousViewNew();

        private void OnShowViewSignal(ShowViewSignalVo signal) =>
            ShowView(new UIManagerViewDataVo(signal.ViewRegistryType, signal.ViewId, signal.ViewerLayer, signal.IsOverlay));

        private void InitializeMainViews()
        {
            foreach (var viewDataVo in viewRegistryData)
            {
                if (!viewDataVo.viewRegistry)
                    throw new NullReferenceException("View registry not set for " + viewDataVo.type);

                RegisterView(viewDataVo.type, viewDataVo.viewRegistry);
            }

            if (debug) Log.Info("initialized global views: " + _viewsRegistry.Count);
        }

        private void RegisterView(ViewRegistryType registryType, IUIViewRegistry registry) =>
            _viewsRegistry.TryAdd(registryType, registry);

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<ShowViewSignalVo>(OnShowViewSignal);
            _signalBus.Unsubscribe<ShowPreviousViewSignalVo>(OnSwitchToPreviousViewSignal);
        }
    }
}
