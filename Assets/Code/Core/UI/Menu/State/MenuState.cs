using Code.Core.FSM;
using Code.Core.UI._Base;
using Code.Core.UI._Base.Model;
using JetBrains.Annotations;

namespace Code.Core.UI.Menu.State
{
    public enum MenuStateType
    {
        Main,
        Settings
    }

    [UsedImplicitly]
    public sealed class MenuState : UIStateBase<IMenuModel, MenuStateType>
    {
        protected override void InitializeSubStates()
        {
            var main = new UISubStateBaseData(UIManager, GameStateType.Menu);

            var settings = new UISubStateBaseData(UIManager, GameStateType.Menu);

            SubStatesCache.TryAdd(MenuStateType.Main, new MenuStateMain(main, MenuStateType.Main));
            SubStatesCache.TryAdd(MenuStateType.Settings, new MenuSettingsUISubState(settings, MenuStateType.Settings));
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
