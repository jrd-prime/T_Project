using System;
using Game.UI._Base.ViewModel;
using R3;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UI._Base.Component
{
    /// <summary>
    /// База компонента вьюва.
    /// Например, в геймплей вьюве есть хелсбар, бар опыта, которые являются частью вью (in uidocument).
    /// Не путать с сабвью.
    /// </summary>
    public abstract class ViewComponentBase<TUIViewModel> where TUIViewModel : IUIViewModel, IDisposable
    {
        protected readonly VisualElement Root;
        protected readonly TUIViewModel ViewModel;
        protected readonly CompositeDisposable Disposables = new();

        protected ViewComponentBase(ViewComponentBaseData<TUIViewModel> data)
        {
            Root = data.Root;
            ViewModel = data.ViewModel;

            InitComponent();
        }

        // TODO подумать нужен ли тут резолвер
        private void InitComponent()
        {
            Debug.LogWarning("InitComponent " + GetType().Name);
            InitializeVisualElements();
            Localize();
            InitializeSubscriptions();
        }

        /// <summary>
        /// Localize the view
        /// </summary>
        protected abstract void Localize();

        /// <summary>
        /// Find all visual elements in the view
        /// </summary>
        protected abstract void InitializeVisualElements();

        /// <summary>
        /// InitializeSubscriptions on constructor
        /// </summary>
        protected abstract void InitializeSubscriptions();

        public void Dispose()
        {
            Debug.LogWarning("Dispose " + GetType().Name);
            Disposables?.Dispose();
        }
    }
}
