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
    public abstract class AViewRegistryBase : MonoBehaviour, IUIViewRegistry
    {
        [SerializeField] public bool inSafeZone;
        [SerializeField] private ViewBaseDataVo[] views;

        protected readonly Dictionary<string, AViewBase> ViewsCache = new();


        private void Awake()
        {
            if (!HasMainView()) throw new Exception("Main view not found in views. " + GetType());
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
            template.ContentToCenter();

            return template;
        }

        /// <summary>
        /// Есть ли вьюшка с ViewIDConst.Main в списке
        /// </summary>
        private bool HasMainView() =>
            views.AsValueEnumerable().Any(viewData => viewData.id == ViewIDConst.Main);

        /// <summary>
        /// Есть ли вьюшка в кеше по id
        /// </summary>
        public bool HasView(string viewId) => ViewsCache.ContainsKey(viewId);
    }
}
