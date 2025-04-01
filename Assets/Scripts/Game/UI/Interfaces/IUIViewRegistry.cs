using UnityEngine.UIElements;

namespace Game.UI.Interfaces
{
    public interface IUIViewRegistry
    {
        TemplateContainer GetView(string viewId);
        bool HasView(string viewId);
    }
}
