using Code.Core.WORK.UIStates._Base.UIStatesTypes;
using Code.Core.WORK.UIStates._Base.View;

namespace Code.Core.WORK.UIStates.Menu.View
{
    public interface IMainMenuView : IUIView
    {
    }

    public class MenuView : CustomUIViewBase<IMenuViewModel, MenuStateType>
    {
    }
}
