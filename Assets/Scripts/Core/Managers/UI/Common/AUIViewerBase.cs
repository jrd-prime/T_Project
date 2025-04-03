using Core.Extensions;
using Core.Managers.UI.Interfaces;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Managers.UI.Common
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class AUIViewerBase : MonoBehaviour, IUIViewer
    {
        [SerializeField] private VisualTreeAsset debugTemplateContainer;

        protected VisualElement DebugContainer { get; private set; }
        protected VisualElement RootVisualElement { get; private set; }
        protected VisualElement RootContainer { get; private set; }
        protected VisualElement MidLayer { get; private set; }
        protected VisualElement DefaultLayer { get; private set; }
        protected VisualElement TopLayer { get; private set; }
        protected ViewerDebugContainer ViewerDebugContainer { get; private set; }


        private void Awake()
        {
            RootVisualElement = GetComponent<UIDocument>().rootVisualElement;

            RootContainer = RootVisualElement.GetVisualElement<VisualElement>("viewer-container", name);
            MidLayer = RootContainer.GetVisualElement<VisualElement>("back-layer", name);
            DefaultLayer = RootContainer.GetVisualElement<VisualElement>("main-layer", name);
            TopLayer = RootContainer.GetVisualElement<VisualElement>("top-layer", name);

            ViewerDebugContainer = new ViewerDebugContainer(debugTemplateContainer);
        }
    }

    public class ViewerDebugContainer
    {
        public VisualElement DebugContainer { get; private set; }
        public Label Text { get; private set; }

        public ViewerDebugContainer(VisualTreeAsset debugTemplateContainer)
        {
            var debugContainerInstance = debugTemplateContainer.Instantiate();
            DebugContainer = debugContainerInstance.GetVisualElement<VisualElement>("debug-container", GetType().Name);
            Text = DebugContainer.GetVisualElement<Label>("text", GetType().Name);
        }
    }
}
