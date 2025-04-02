using System;
using System.Collections.Generic;
using Core.Extensions;
using Db.Data;
using Game.UI.Data;
using Game.UI.Interfaces;
using UnityEngine;
using UnityEngine.UIElements;
using ZLinq;

namespace Game.UI.Common
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class AViewRegistryBase : MonoBehaviour, IUIViewRegistry
    {
        [SerializeField] public bool inSafeZone;
        [SerializeField] private ViewBaseDataVo[] views;

        private VisualElement _viewContainer;
        private VisualElement _root;

        protected readonly Dictionary<string, AViewBase> ViewsCache = new();


        private void Awake()
        {
            if (!HasMainView()) throw new Exception("Main view not found in views. " + GetType());

            _root = GetComponent<UIDocument>().rootVisualElement;

            _viewContainer = _root.GetVisualElement<VisualElement>(UIElementId.MainViewContainerId, GetType().Name);
            _viewContainer.style.left = inSafeZone ? 0 : 100;
            _viewContainer.style.right = inSafeZone ? 0 : 100;
            _viewContainer.style.top = inSafeZone ? 0 : 100;
            _viewContainer.style.bottom = inSafeZone ? 0 : 100;
            _viewContainer.style.display = DisplayStyle.None;

            RegisterViews();
        }

        /// <summary>
        /// Добавляет вьюшки в кэш с id
        /// </summary>
        private void RegisterViews()
        {
            foreach (var subView in views) ViewsCache.Add(subView.id, subView.view);
        }

        /// <summary>
        /// Получить VisualElement вью по id
        /// </summary>
        public VisualElement GetView(string viewId)
        {
            if (ViewsCache.Count == 0) throw new Exception("ViewsCache is empty. " + GetType());

            if (!ViewsCache.TryGetValue(viewId, out var view))
                throw new KeyNotFoundException($"View not found in cache for: {viewId} / {GetType()}");

            var template = view.GetTemplate();

            _viewContainer.Clear();
            _viewContainer.Add(template);

            return _viewContainer;
        }

        /// <summary>
        /// Есть ли вьюшка с ViewConst.MainViewId в списке
        /// </summary>
        private bool HasMainView() =>
            views.AsValueEnumerable().Any(viewData => viewData.id == ViewConst.MainViewId);

        /// <summary>
        /// Есть ли вьюшка в кеше по id
        /// </summary>
        public bool HasView(string viewId) => ViewsCache.ContainsKey(viewId);
    }
}
