using System;
using Code.Core.WORK.UIStates._Base.Data;
using Code.Tools;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.Core.WORK.UIManager.Viewer
{
    public class UIViewer : UIViewerBase
    {
        private void ToSafe()
        {
            var safeZoneOffset = ScreenHelper.GetSafeZoneOffset(800f, 360f);
            RootVisualElement.style.marginLeft = safeZoneOffset.x >= 16 ? safeZoneOffset.x : 16;
            RootVisualElement.style.marginTop = safeZoneOffset.y;
        }

        private void Prepare(SubViewDto subViewDto)
        {
            var view = subViewDto.Template;
            if (view == null) throw new NullReferenceException("View is null.");
            view.pickingMode = PickingMode.Ignore;

            if (subViewDto.InSafeZone) ToSafe();

            view.style.position = Position.Absolute;
            view.style.left = 0;
            view.style.top = 0;
            view.style.right = 0;
            view.style.bottom = 0;
        }

        private void ClearAll()
        {
            BackLayer.Clear();
            MainLayer.Clear();
            TopLayer.Clear();
        }

        public void ShowNewBase(SubViewDto subViewDto)
        {
            Debug.Log("<color=yellow>[VIEWER BASE]</color> Show view ");
            Prepare(subViewDto);
            HideView();
            MainLayer.Add(subViewDto.Template);
        }

        public void ShowOverSubView(SubViewDto subViewDto)
        {
            Debug.Log("<color=yellow>[VIEWER OVER]</color> Show view");
            Prepare(subViewDto);
            TopLayer.Add(subViewDto.Template);
        }

        public void ShowUnderSubView(SubViewDto subViewDto)
        {
            Debug.Log("<color=yellow>[VIEWER UNDER]</color> Show view");
            Prepare(subViewDto);
            BackLayer.Add(subViewDto.Template);
        }

        public void HideView() => ClearAll();
    }
}
