using Code.Core.UI._Base.View;
using Code.Core.UI._Base.ViewStateTypes;

namespace Code.Core.UI.Menu.View
{
    public interface IMainMenuView : IUIView
    {
    }

    public class MenuView : CustomViewBase<IMenuViewModel, MenuStateType>
    {
    }
}
