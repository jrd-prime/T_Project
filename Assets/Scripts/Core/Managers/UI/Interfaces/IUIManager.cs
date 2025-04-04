using System.Collections.Generic;
using Core.Managers.UI.Impls;
using Game.UI.Data;

namespace Core.Managers.UI.Interfaces
{
    public interface IUIManager
    {
        void HideView(string viewId);
        void HideAllViews();

        void ShowViewNew(ViewRegistryType type, string viewId, UIViewer.Layer layer = UIViewer.Layer.Default,
            bool isOverlay = false);

        Stack<(string viewId, UIViewer.Layer layer)> _viewStack { get; }
        bool IsMainViewActive(ViewRegistryType type);
        void ShowPreviousViewNew();
        bool IsViewActive(string viewId);
        bool IsOverlayViewActive();
        void CloseOverlayView();
    }
}
