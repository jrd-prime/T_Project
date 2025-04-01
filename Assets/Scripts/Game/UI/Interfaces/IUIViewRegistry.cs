using UnityEngine.UIElements;

namespace Game.UI.Interfaces
{
    public interface IUIViewRegistry
    {
        VisualElement GetView(string viewId);
        bool HasView(string viewId);
    }
}
