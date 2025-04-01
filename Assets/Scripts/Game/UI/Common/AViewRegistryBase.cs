using System;
using System.Collections.Generic;
using Core.Extensions;
using Db.Data;
using Game.UI.Common.Base.Data;
using Game.UI.Data;
using Game.UI.Interfaces;
using ModestTree;
using UnityEngine;
using UnityEngine.UIElements;
using ZLinq;

namespace Game.UI.Common
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class AViewRegistryBase : MonoBehaviour, IUIViewRegistry
    {
        [SerializeField] private SubViewBaseDataVo[] subViews;
        [SerializeField] public bool inSafeZone;

        protected readonly Dictionary<string, AViewBase> SubViewsCache = new();
        protected VisualElement ViewContainer { get; private set; }

        private VisualElement _root;


        private void Awake()
        {
            if (!HasMainSubView()) throw new Exception("Main view not found in sub views. " + GetType());
            _root = GetComponent<UIDocument>().rootVisualElement;

            ViewContainer = _root.GetVisualElement<VisualElement>(UIElementId.MainViewContainerId, GetType().Name);
            ViewContainer.style.left = inSafeZone ? 0 : 100;
            ViewContainer.style.right = inSafeZone ? 0 : 100;
            ViewContainer.style.top = inSafeZone ? 0 : 100;
            ViewContainer.style.bottom = inSafeZone ? 0 : 100;

            foreach (var subView in subViews) RegisterSubView(subView);

            Hide();
        }

        private void RegisterSubView(SubViewBaseDataVo subView) => SubViewsCache.Add(subView.subViewId, subView.view);
        private bool HasMainSubView() => subViews.AsValueEnumerable().Any(viewData => viewData.subViewId == "main");

        public SubViewTemplateData GetSubViewTemplateData(string subState)
        {
            var subView = GetSubView(subState) ?? throw new NullReferenceException("SubView is null. " + subState);

            return new SubViewTemplateData
            {
                Template = subView.GetTemplate()
            };
        }

        private AViewBase GetSubView(string viewId)
        {
            if (SubViewsCache.Count == 0) throw new Exception("SubViewsCache is empty. " + GetType());

            if (!SubViewsCache.TryGetValue(viewId, out var subViewBase))
                throw new KeyNotFoundException($"SubView not found in cache for: {viewId}");

            return subViewBase;
        }


        public void Show()
        {
            Log.Info("Show main view for " + GetType());
            _root.style.display = DisplayStyle.Flex;
        }

        public void Hide()
        {
            Log.Info("Hide main view for " + GetType());
            _root.style.display = DisplayStyle.None;
        }

        public VisualElement GetView(string viewId)
        {
            var view = GetSubView(viewId) ?? throw new NullReferenceException("SubView is null. " + viewId);

            ViewContainer.Clear();
            ViewContainer.Add(view.GetTemplate());

            if (ViewContainer == null) throw new NullReferenceException("ViewContainer is null. " + name);

            return ViewContainer;
        }

        public bool HasView(string viewId) => SubViewsCache.ContainsKey(viewId);
    }
}
