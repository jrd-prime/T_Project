using System;
using Core.Extensions;
using Core.Managers.UI.Interfaces;
using Game.UI.Common.Base.Data;
using ModestTree;
using Tools;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Managers.UI.Impls
{
    /// <summary>
    /// Показывает вьюшки по слоям
    /// </summary>
    [RequireComponent(typeof(UIDocument))]
    public sealed class UIRenderer : MonoBehaviour, IUIViewer
    {
        [SerializeField] private bool debug = true;
        [SerializeField] private VisualTreeAsset debugTemplate;

        private VisualElement _debugContainer;
        private VisualElement _rootVisualElement;
        private VisualElement _defaultLayer;
        private VisualElement _topLayer;
        private UIRendererDebugContainer _uiRendererDebugContainer;
        private VisualElement _rootContainer;

        private void Awake()
        {
            if (!debugTemplate) throw new NullReferenceException(nameof(debugTemplate));

            _rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

            _rootContainer = _rootVisualElement.GetVisualElement<VisualElement>("viewer-container", name);
            _defaultLayer = _rootContainer.GetVisualElement<VisualElement>("default-layer", name);
            _topLayer = _rootContainer.GetVisualElement<VisualElement>("top-layer", name);

            if (debug) _uiRendererDebugContainer = new UIRendererDebugContainer(debugTemplate, "debug-container");
        }

        public void ShowView(ViewTemplateData data, Layer layer)
        {
            if (debug) Log.Info($"<color=green>[VIEWER]</color> ... {data.StateId} /  {data.ViewId} (layer: {layer})");

            PrepareTemplate(data);

            switch (layer)
            {
                case Layer.Default: _defaultLayer.Add(data.Template); break;
                case Layer.Top: _topLayer.Add(data.Template); break;
                default: throw new ArgumentOutOfRangeException(nameof(layer), layer, null);
            }
        }

        public void HideView() => ClearAll();

        public void ClearLayer(Layer layer)
        {
            switch (layer)
            {
                case Layer.Default: _defaultLayer.Clear(); break;
                case Layer.Top: _topLayer.Clear(); break;
                default: throw new ArgumentOutOfRangeException(nameof(layer), layer, null);
            }
        }

        private void PrepareTemplate(ViewTemplateData viewTemplateData)
        {
            var template = viewTemplateData.Template;

            if (viewTemplateData.InSafeZone) ToSafe();

            template.SetFullScreen();
            template.pickingMode = PickingMode.Ignore;
            template.style.display = DisplayStyle.Flex;

            if (debug) AddDebug(template, viewTemplateData);
        }

        private void ClearAll()
        {
            _defaultLayer.Clear();
            _topLayer.Clear();
        }

        private void AddDebug(VisualElement view, ViewTemplateData data)
        {
            _uiRendererDebugContainer.Text.text =
                $"{data.StateId} /  {data.ViewId}. {data.UIViewerDebugData.Name}: {data.UIViewerDebugData.ViewStackCount.ToString()}. Overlay: {data.UIViewerDebugData.IsOverlay.ToString()}";
            view.Add(_uiRendererDebugContainer.DebugContainer);
        }

        private void ToSafe()
        {
            var safeZoneOffset = ScreenHelper.GetSafeZoneOffset(800f, 360f);
            _rootVisualElement.style.marginLeft = safeZoneOffset.x >= 16 ? safeZoneOffset.x : 16;
            _rootVisualElement.style.marginTop = safeZoneOffset.y;
        }

        public enum Layer
        {
            Default,
            Top
        }

        private class UIRendererDebugContainer
        {
            public VisualElement DebugContainer { get; }
            public Label Text { get; private set; }

            public UIRendererDebugContainer(VisualTreeAsset debugTemplate, string debugContainerName)
            {
                var debugContainerInstance = debugTemplate.Instantiate();
                DebugContainer =
                    debugContainerInstance.GetVisualElement<VisualElement>(debugContainerName, GetType().Name);
                Text = DebugContainer.GetVisualElement<Label>("text", GetType().Name);
            }
        }
    }
}
