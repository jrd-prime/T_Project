using System;
using System.Collections.Generic;
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
        private VisualElement _root;

        private void Awake()
        {
            if (!HasMainSubView()) throw new Exception("Main view not found in sub views. " + GetType());
            _root = GetComponent<UIDocument>().rootVisualElement;

            foreach (var subView in subViews) RegisterSubView(subView);

            Hide();
        }

        private void RegisterSubView(SubViewBaseDataVo subView) =>
            SubViewsCache.Add(subView.subViewId, subView.view);

        private bool HasMainSubView() => subViews.AsValueEnumerable().Any(viewData => viewData.subViewId == "main");


        public SubViewTemplateData GetSubViewTemplateData(string subState)
        {
            var subView = GetSubView(subState) ?? throw new NullReferenceException("SubView is null. " + subState);

            return new SubViewTemplateData
            {
                Template = subView.GetTemplate()
            };
        }

        private AViewBase GetSubView(string subState)
        {
            if (!SubViewsCache.TryGetValue(subState, out var subViewBase))
                throw new KeyNotFoundException($"SubView not found in cache for: {subState}");

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

        public TemplateContainer GetView(string viewId)
        {
            var view = GetSubView(viewId) ?? throw new NullReferenceException("SubView is null. " + viewId);
            return view.GetTemplate();
        }

        public bool HasView(string viewId) => SubViewsCache.ContainsKey(viewId);
    }
}
