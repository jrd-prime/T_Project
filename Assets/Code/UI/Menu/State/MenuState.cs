using Code.Core.FSM;
using Code.UI._Base;
using Code.UI._Base.Model;
using JetBrains.Annotations;

namespace Code.UI.Menu.State
{
    public enum MenuStateType
    {
        Main,
        Settings,
        AudioSettings,
        VideoSettings
    }

    [UsedImplicitly]
    public sealed class MenuState : UIStateBase<IMenuModel, MenuStateType>
    {
        protected override void InitializeSubStates()
        {
            var stateBaseData = new UISubStateBaseData(UIManager, GameStateType.Menu);

            RegisterSubState(MenuStateType.Main, new MenuStateMain(stateBaseData, MenuStateType.Main));
            RegisterSubState(MenuStateType.Settings, new MenuStateSettings(stateBaseData, MenuStateType.Settings));
            RegisterSubState(MenuStateType.AudioSettings,
                new MenuStateSettingsAudio(stateBaseData, MenuStateType.AudioSettings));
            RegisterSubState(MenuStateType.VideoSettings,
                new MenuStateSettingsVideo(stateBaseData, MenuStateType.VideoSettings));
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
