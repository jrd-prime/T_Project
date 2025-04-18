﻿using System;
using System.Collections.Generic;
using Game.Extensions;
using Game.UI.Data;
using Game.UI.Interfaces;
using R3;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UI.Common
{
    public abstract class AViewBase : MonoBehaviour, IUISubView
    {
        [SerializeField] protected string ttlLocalizationId;
        [SerializeField] protected VisualTreeAsset template;

        protected TemplateContainer Template;
        protected VisualElement RootContainer;
        protected bool IsInitialized;
        protected Label ViewMainHeader;

        protected readonly Dictionary<Button, EventCallback<ClickEvent>> CallbacksCache = new();
        protected readonly CompositeDisposable Disposables = new();
        protected VisualElement ContentContainer;

        private void Awake()
        {
            if (template == null) throw new NullReferenceException("Template is null. " + name);

            Template = template.Instantiate();
            Template.SetFullScreen();

            RootContainer = Template.GetVisualElement<VisualElement>(UIElementId.ContainerId, name);
            ContentContainer = RootContainer.GetVisualElement<VisualElement>(UIElementId.ContainerId, name);

            ViewMainHeader = RootContainer.GetVisualElement<Label>(UIElementId.TitleId, name);
            InitializeView();

            IsInitialized = true;
        }

        public TemplateContainer GetTemplate()
        {
            if (!IsInitialized) throw new Exception("View is not initialized. " + name);

            return Template ?? throw new NullReferenceException("Template is null. " + name);
        }

        /// <summary>
        /// Register initialized callbacks
        /// </summary>
        protected void RegisterCallbacks()
        {
            if (CallbacksCache.Count == 0) return;

            foreach (var callback in CallbacksCache)
                callback.Key.RegisterCallback(callback.Value);
        }

        /// <summary>
        /// Find and initialize UI elements
        /// </summary>
        protected abstract void InitializeView();

        /// <summary>
        /// In Start() cuz ViewModel injected in start //TODO or refact
        /// </summary>
        protected abstract void CreateAndInitComponents();

        /// <summary>
        /// Localize
        /// </summary>
        protected abstract void Localize();

        /// <summary>
        /// Add callbacks to UI elements
        /// </summary>
        protected abstract void InitializeCallbacks();


        /// <summary>
        /// Unregister initialized callbacks
        /// </summary>
        private void UnregisterCallbacks()
        {
            if (CallbacksCache.Count == 0) return;

            foreach (var callback in CallbacksCache)
                callback.Key.UnregisterCallback(callback.Value);
        }

        private void OnDestroy()
        {
            UnregisterCallbacks();
            Disposables?.Dispose();
        }
    }
}
