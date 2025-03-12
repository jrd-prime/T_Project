using System;
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

        private void Awake()
        {
            Root = GetComponent<UIDocument>().rootVisualElement;
        }

        private void OnEnable()
        {
            Debug.LogWarning("on enable  ui view base. Name: " + name);
            InitializeVisualElements();
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        protected abstract void UnsubscribeFromEvents();


        protected abstract void SubscribeToEvents();
        protected abstract void InitializeVisualElements();
        
        public abstract void Show();
        public abstract void Hide();
    }
}
