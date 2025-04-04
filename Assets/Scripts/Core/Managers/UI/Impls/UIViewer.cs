using System;
using Core.Extensions;
using Core.Managers.UI.Common;
using Game.UI.Common.Base.Data;
using ModestTree;
using Tools;
using UnityEngine.UIElements;

namespace Core.Managers.UI.Impls
{
    /// <summary>
    /// Показывает вьюшки по слоям
    /// </summary>
    public sealed class UIViewer : AUIViewerBase
    {
        private void PrepareTemplate(ViewTemplateData viewTemplateData)
        {
            var template = viewTemplateData.Template;

            if (viewTemplateData.InSafeZone) ToSafe();

            template.SetFullScreen();
            template.pickingMode = PickingMode.Ignore;
            template.style.display = DisplayStyle.Flex;

            AddDebug(template, viewTemplateData);
        }

        public void ShowView(ViewTemplateData data, Layer layer)
        {
            Log.Info($"<color=green>[VIEWER]</color> ... {data.StateId} /  {data.ViewId} (layer: {layer})");

            PrepareTemplate(data);

            switch (layer)
            {
                case Layer.Default: DefaultLayer.Add(data.Template); break;
                case Layer.Top: TopLayer.Add(data.Template); break;
                default: throw new ArgumentOutOfRangeException(nameof(layer), layer, null);
            }
        }

        public void HideView() => ClearAll();

        public void ClearLayer(Layer layer)
        {
            switch (layer)
            {
                case Layer.Default: DefaultLayer.Clear(); break;
                case Layer.Top: TopLayer.Clear(); break;
                default: throw new ArgumentOutOfRangeException(nameof(layer), layer, null);
            }
        }

        private void ClearAll()
        {
            DefaultLayer.Clear();
            TopLayer.Clear();
        }

        private void AddDebug(VisualElement view, ViewTemplateData data)
        {
            ViewerDebugContainer.Text.text =
                $"{data.StateId} /  {data.ViewId}. {data.UIViewerDebugData.Name}: {data.UIViewerDebugData.ViewStackCount.ToString()}. Overlay: {data.UIViewerDebugData.IsOverlay.ToString()}";
            view.Add(ViewerDebugContainer.DebugContainer);
        }

        private void ToSafe()
        {
            var safeZoneOffset = ScreenHelper.GetSafeZoneOffset(800f, 360f);
            RootVisualElement.style.marginLeft = safeZoneOffset.x >= 16 ? safeZoneOffset.x : 16;
            RootVisualElement.style.marginTop = safeZoneOffset.y;
        }

        public enum Layer
        {
            Default,
            Top
        }
    }
}
