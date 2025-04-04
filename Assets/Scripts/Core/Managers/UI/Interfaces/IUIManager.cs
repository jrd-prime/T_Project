using Core.Managers.UI.Impls;
using Game.UI.Data;

namespace Core.Managers.UI.Interfaces
{
    public interface IUIManager
    {
        void ShowView(UIManagerViewDataVo data);
        bool IsMainViewActive(ViewRegistryType registryType);
        void ShowPreviousViewNew();
        bool IsViewActive(string viewId);
        bool IsOverlayViewActive();
        void CloseOverlayView();
        bool IsOverlayIt(string viewId);
    }
}
