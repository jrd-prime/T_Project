using Code.Core.WORK.UI.Base.View;
using Code.Core.WORK.UIStates._Base.UIStatesTypes;

namespace Code.Core.WORK.UIStates.Menu.View
{
    public interface IMainMenuView : IUIView
    {
    }

    public class MenuView : CustomUIViewBase<IMenuViewModel, MenuStateType>
    {
    }
}
