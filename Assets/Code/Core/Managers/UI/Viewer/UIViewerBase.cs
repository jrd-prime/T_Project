using System;
using Code.Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.Core.Managers.UI.Viewer
{
    public interface IUIViewer
    {
    }

    [RequireComponent(typeof(UIDocument))]
    public class UIViewerBase : MonoBehaviour, IUIViewer
    {
        protected VisualElement RootVisualElement { get; private set; }
        protected VisualElement RootContainer { get; private set; }
        protected VisualElement BackLayer { get; private set; }
        protected VisualElement MainLayer { get; private set; }
        protected VisualElement TopLayer { get; private set; }

        private void Awake()
        {
            RootVisualElement = GetComponent<UIDocument>().rootVisualElement;
            RootContainer = RootVisualElement.Q<VisualElement>("viewer-container") ??
                            throw new NullReferenceException("Viewer container not found");

            BackLayer = RootContainer.GetVisualElement<VisualElement>("back-layer", name);
            MainLayer = RootContainer.GetVisualElement<VisualElement>("main-layer", name);
            TopLayer = RootContainer.GetVisualElement<VisualElement>("top-layer", name);
        }
    }
}
