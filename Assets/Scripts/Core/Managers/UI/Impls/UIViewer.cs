using System;
using Core.Extensions;
using Core.Managers.UI.Common;
using Game.UI.Common.Base.Data;
using Tools;
using UnityEngine;
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
            ViewerDebugContainer.Text.text = $"{data.StateId} /  {data.ViewId}";
            view.Add(ViewerDebugContainer.DebugContainer);
        }

        public void ShowNewBase(ViewTemplateData viewTemplateData)
        {
            Debug.Log("<color=yellow>[VIEWER MAIN]</color> Show view");
            Prepare(viewTemplateData);
            MainLayer.Add(viewTemplateData.Template);
        }

        public void ShowOverSubView(ViewTemplateData viewTemplateData)
        {
            Debug.Log("<color=yellow>[VIEWER OVER]</color> Show view");
            Prepare(viewTemplateData);
            TopLayer.Add(viewTemplateData.Template);
        }

        public void ShowUnderSubView(ViewTemplateData viewTemplateData)
        {
            Debug.Log("<color=yellow>[VIEWER UNDER]</color> Show view");
            Prepare(viewTemplateData);
            BackLayer.Add(viewTemplateData.Template);
        }

        public void HideView() => ClearAll();

        public void ClearLayer(Layer layer)
        {
            switch (layer)
            {
                case Layer.Back:
                    BackLayer.Clear();
                    break;
                case Layer.Main:
                    MainLayer.Clear();
                    break;
                case Layer.Top:
                    TopLayer.Clear();
                    break;
            }
        }

        private void ClearAll()
        {
            BackLayer.Clear();
            MainLayer.Clear();
            TopLayer.Clear();
        }

        public enum Layer
        {
            Back,
            Main,
            Top
        }
    }
}
