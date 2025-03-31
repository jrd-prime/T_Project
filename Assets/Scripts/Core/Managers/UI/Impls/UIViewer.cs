using System;
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

        private void Prepare(SubViewTemplateData subViewTemplateData)
        {
            var view = subViewTemplateData.Template;
            if (view == null) throw new NullReferenceException("View is null.");
            view.pickingMode = PickingMode.Ignore;

            if (subViewTemplateData.InSafeZone) ToSafe();

            view.style.position = Position.Absolute;
            view.style.left = 0;
            view.style.top = 0;
            view.style.right = 0;
            view.style.bottom = 0;
        }

        public void ShowNewBase(SubViewTemplateData subViewTemplateData)
        {
            Debug.Log("<color=yellow>[VIEWER MAIN]</color> Show view");
            Prepare(subViewTemplateData);
            MainLayer.Add(subViewTemplateData.Template);
        }

        public void ShowOverSubView(SubViewTemplateData subViewTemplateData)
        {
            Debug.Log("<color=yellow>[VIEWER OVER]</color> Show view");
            Prepare(subViewTemplateData);
            TopLayer.Add(subViewTemplateData.Template);
        }

        public void ShowUnderSubView(SubViewTemplateData subViewTemplateData)
        {
            Debug.Log("<color=yellow>[VIEWER UNDER]</color> Show view");
            Prepare(subViewTemplateData);
            BackLayer.Add(subViewTemplateData.Template);
        }

        public void HideView() => ClearAll();

        public void ClearLayer(UIManager.ViewLayer layer)
        {
            switch (layer)
            {
                case UIManager.ViewLayer.Back:
                    BackLayer.Clear();
                    break;
                case UIManager.ViewLayer.Main:
                    MainLayer.Clear();
                    break;
                case UIManager.ViewLayer.Top:
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
    }
}