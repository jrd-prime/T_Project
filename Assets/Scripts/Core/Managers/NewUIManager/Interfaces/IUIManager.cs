using Core.FSM.Data;

namespace Core.HSM
{
    public interface IUIManager
    {
        void ShowView(string viewId);
        void HideView(string viewId);
        void HideAllViews();
        void SwitchToView(string viewId);
        void ShowPreviousView();
        void SetBaseView(string viewId);
    }
}
