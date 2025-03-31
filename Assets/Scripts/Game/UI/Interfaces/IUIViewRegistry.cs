using UnityEngine.UIElements;

namespace Game.UI.Interfaces
{
    public interface IUIViewRegistry
    {
        TemplateContainer GetView(string viewId);
    }
}
