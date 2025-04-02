using Core.Managers.UI.Impls;
using Game.UI.Data;

namespace Core.Managers.UI.Interfaces
{
    public interface IUIManager
    {
        void HideView(string viewId);
        void HideAllViews();
        void SwitchToView(string viewId);
        void SetAndShowBaseView(ViewRegistryType type);
        void ShowPreviousView();
        bool IsGameplayMainViewActive();
        void ShowView(ViewRegistryType type, string viewId, UIViewer.Layer layer, bool replace);
    }
}
