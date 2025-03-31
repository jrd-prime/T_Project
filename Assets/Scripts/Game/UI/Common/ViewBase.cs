using System;
using System.Collections.Generic;
using Game.UI._old.Base.Data;
using Game.UI.Interfaces;
using ModestTree;
using UnityEngine;
using UnityEngine.UIElements;
using ZLinq;

namespace Game.UI.Common
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class ViewBase : MonoBehaviour, IUIView
    {
        [SerializeField] private SubViewBaseDataVo[] subViews;

        protected readonly Dictionary<string, SubViewBase> SubViewsCache = new();
        private VisualElement _root;

        private void Awake()
        {
            if (!HasMainSubView()) throw new Exception("Main view not found in sub views. " + GetType());
            _root = GetComponent<UIDocument>().rootVisualElement;

            foreach (var subView in subViews) RegisterSubView(subView);

            Hide();
        }

        private void RegisterSubView(SubViewBaseDataVo subView) =>
            SubViewsCache.Add(subView.subViewId, subView.subView);

        private bool HasMainSubView() => subViews.AsValueEnumerable().Any(viewData => viewData.subViewId == "main");


        public SubViewTemplateData GetSubViewTemplateData(string subState)
        {
            var subView = GetSubView(subState) ?? throw new NullReferenceException("SubView is null. " + subState);

            return new SubViewTemplateData
            {
                InSafeZone = subView.inSafeZone,
                Template = subView.GetTemplate()
            };
        }

        private SubViewBase GetSubView(string subState)
        {
            if (!SubViewsCache.TryGetValue(subState, out var subViewBase))
                throw new KeyNotFoundException($"SubView not found in cache for: {subState}");

            return subViewBase;
        }


        public void Show()
        {
            Log.Warn("Show main view for " + GetType());
            _root.style.display = DisplayStyle.Flex;
        }

        public void Hide()
        {
            Log.Warn("Hide main view for " + GetType());
            _root.style.display = DisplayStyle.None;
        }
    }
}
