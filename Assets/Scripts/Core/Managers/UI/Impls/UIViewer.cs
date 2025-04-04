using System;
using Core.Extensions;
using Core.Managers.UI.Common;
using Game.UI.Common.Base.Data;
using ModestTree;
using Tools;
using UnityEngine.UIElements;

namespace Core.Managers.UI.Impls
{
    public sealed class UIViewer : AUIViewerBase
    {
        private void ToSafe()
        {
            var safeZoneOffset = ScreenHelper.GetSafeZoneOffset(800f, 360f);
            RootVisualElement.style.marginLeft = safeZoneOffset.x >= 16 ? safeZoneOffset.x : 16;
            RootVisualElement.style.marginTop = safeZoneOffset.y;
        }

        private void Prepare(ViewTemplateData viewTemplateData)
        {
            var view = viewTemplateData.Template ?? throw new NullReferenceException("View is null.");

            view.pickingMode = PickingMode.Ignore;

            if (viewTemplateData.InSafeZone) ToSafe();

            view.SetFullScreen();
            view.style.display = DisplayStyle.Flex;

            AddDebug(view, viewTemplateData);
        }

        private void AddDebug(VisualElement view, ViewTemplateData data)
        {
            ViewerDebugContainer.Text.text =
                $"{data.StateId} /  {data.ViewId}. {data.DebugData.Name}: {data.DebugData.ViewStackCount.ToString()}. Overlay: {data.DebugData.IsOverlay.ToString()}";
            view.Add(ViewerDebugContainer.DebugContainer);
        }

        public void ShowView(ViewTemplateData data, Layer layer)
        {
            Log.Info($"<color=green>[VIEWER]</color> ... {data.StateId} /  {data.ViewId} (layer: {layer})");
            Prepare(data);

            switch (layer)
            {
                case Layer.Default: DefaultLayer.Add(data.Template); break;
                case Layer.Mid: MidLayer.Add(data.Template); break;
                case Layer.Top: TopLayer.Add(data.Template); break;
                default: throw new ArgumentOutOfRangeException(nameof(layer), layer, null);
            }
        }

        public void HideView() => ClearAll();

        public void ClearLayer(Layer layer)
        {
            Log.Warn("clear layer = " + layer);
            switch (layer)
            {
                case Layer.Default: DefaultLayer.Clear(); break;
                case Layer.Mid: MidLayer.Clear(); break;
                case Layer.Top: TopLayer.Clear(); break;
                default: throw new ArgumentOutOfRangeException(nameof(layer), layer, null);
            }
        }

        private void ClearAll()
        {
            Log.Warn("clear all layers");
            DefaultLayer.Clear();
            MidLayer.Clear();
            TopLayer.Clear();
        }

        public enum Layer
        {
            Default,
            Mid,
            Top
        }
    }
}
