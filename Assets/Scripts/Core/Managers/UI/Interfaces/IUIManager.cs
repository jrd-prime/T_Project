using System.Collections.Generic;
using Core.Managers.UI.Impls;
using Game.UI.Data;

namespace Core.Managers.UI.Interfaces
{
    public interface IUIManager
    {
        void HideView(string viewId);
        void HideAllViews();
        void SwitchToView(string viewId);
        void SetAndShowBaseView(ViewRegistryType type, string viewId);

        void ShowViewNew(ViewRegistryType type, string viewId, UIViewer.Layer layer = UIViewer.Layer.Default,
            bool isOverlay = false);

        void ShowView1(ViewRegistryType type, string viewId);
        bool IsGameplayMainViewActive();
        void ShowView1(ViewRegistryType type, string viewId, UIViewer.Layer layer, bool replace, bool isOverlay);
        Stack<(string viewId, UIViewer.Layer layer)> _viewStack { get; }
        bool IsMainViewActive(ViewRegistryType type);
        void ShowPreviousViewNew();
        bool IsViewActive(string viewId);
    }
}
