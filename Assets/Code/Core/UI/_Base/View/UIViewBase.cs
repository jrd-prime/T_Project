using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.Core.UI._Base.View
{
    public interface IUIView
    {
        public void Show();
        public void Hide();
    }

    [RequireComponent(typeof(UIDocument))]
    public abstract class UIViewBase : MonoBehaviour, IUIView
    {
        protected VisualElement Root { get; private set; }

        protected bool IsInitialized;

        protected readonly Dictionary<Button, EventCallback<ClickEvent>> CallbacksCache = new();
        protected readonly CompositeDisposable Disposables = new();

        private void Awake() => Root = GetComponent<UIDocument>().rootVisualElement;

        /// <summary>
        /// Register initialized callbacks
        /// </summary>
        protected void RegisterCallbacks()
        {
            if (CallbacksCache.Count == 0)
            {
                Debug.LogWarning("No callbacks to register. " + name);
                return;
            }

            foreach (var callback in CallbacksCache)
                callback.Key.RegisterCallback(callback.Value);
        }

        /// <summary>
        /// Unregister initialized callbacks
        /// </summary>
        private void UnregisterCallbacks()
        {
            if (CallbacksCache.Count == 0)
            {
                Debug.LogWarning("No callbacks to unregister. " + name);
                return;
            }

            foreach (var callback in CallbacksCache)
                callback.Key.UnregisterCallback(callback.Value);
        }

        /// <summary>
        /// Find and initialize UI elements
        /// </summary>
        protected abstract void InitializeViewElements();

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

        public void Show()
        {
            Root.style.display = DisplayStyle.Flex;
            OnShow();
        }

        public void Hide()
        {
            Root.style.display = DisplayStyle.None;
            OnHide();
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }

        private void OnDestroy()
        {
            Disposables?.Dispose();
            UnregisterCallbacks();
        }
    }
}
