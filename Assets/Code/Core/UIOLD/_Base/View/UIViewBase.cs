using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.Core.UIOLD._Base.View
{
    public interface IUIView
    {
        void Show();
        void Hide();
        Observable<UIViewBase> OnShowRequested { get; }
        Observable<Unit> OnHideRequested { get; }
    }

    [RequireComponent(typeof(UIDocument))]
    public abstract class UIViewBase : MonoBehaviour, IUIView
    {
        protected VisualElement Root { get; private set; }
        protected bool IsInitialized;

        protected readonly Dictionary<Button, EventCallback<ClickEvent>> CallbacksCache = new();
        protected readonly CompositeDisposable Disposables = new();

        private readonly Subject<UIViewBase> _onShowRequested = new();
        private readonly Subject<Unit> _onHideRequested = new();

        public Observable<UIViewBase> OnShowRequested => _onShowRequested;
        public Observable<Unit> OnHideRequested => _onHideRequested;

        private void Awake() => Root = GetComponent<UIDocument>().rootVisualElement;

        protected void RegisterCallbacks()
        {
            if (CallbacksCache.Count == 0) return;
            foreach (var callback in CallbacksCache)
                callback.Key.RegisterCallback(callback.Value);
        }

        private void UnregisterCallbacks()
        {
            if (CallbacksCache.Count == 0) return;
            foreach (var callback in CallbacksCache)
                callback.Key.UnregisterCallback(callback.Value);
        }

        protected abstract void InitializeViewElements();
        protected abstract void CreateAndInitComponents();
        protected abstract void Localize();
        protected abstract void InitializeCallbacks();

        public virtual void Show()
        {
            if (!IsInitialized)
            {
                InitializeViewElements();
                CreateAndInitComponents();
                Localize();
                InitializeCallbacks();
                RegisterCallbacks();
                IsInitialized = true;
            }

            Root.style.display = DisplayStyle.Flex;
            OnShow();
        }

        public virtual void Hide()
        {
            Root.style.display = DisplayStyle.None;
            Disposables.Clear();
            OnHide();
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }

        // Метод для запроса показа другого View
        protected void RequestShow(UIViewBase view)
        {
            _onShowRequested.OnNext(view);
        }

        // Метод для запроса скрытия текущего View
        public void RequestHide()
        {
            _onHideRequested.OnNext(Unit.Default);
        }

        private void OnDestroy()
        {
            Disposables?.Dispose();
            UnregisterCallbacks();
            _onShowRequested?.Dispose();
            _onHideRequested?.Dispose();
        }
    }
}
