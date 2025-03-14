using Code.Core.WORK.JStateMachine;
using Code.Core.WORK.UI.Base.Model;
using Code.Core.WORK.UIStates._Base.UIStatesTypes;

namespace Code.Core.WORK.UIStates.Menu.State
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
