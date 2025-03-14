using Code.Core.FSM;
using Code.Core.UI._Base.Model;
using Code.Core.UI._Base.ViewStateTypes;

namespace Code.Core.UI.Menu.State
{
    public class MenuState : UIStateBase<IMenuModel, MenuStateType>
    {
        protected override void InitializeSubStates()
        {
            SubStatesCache.TryAdd(MenuStateType.Main,
                new MenuStateMain(UIManager, GameStateType.Menu, MenuStateType.Main));
            SubStatesCache.TryAdd(MenuStateType.Settings,
                new MenuSettingsUISubState(UIManager, GameStateType.Menu, MenuStateType.Settings));
        }

        protected override void InitCustomSubscribes()
        {
        }

        protected override void OnBaseStateEnter()
        {
        }

        protected override void OnBaseStateExit()
        {
        }
    }
}
