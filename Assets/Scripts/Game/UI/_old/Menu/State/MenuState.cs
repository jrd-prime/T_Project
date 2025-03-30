using System;
using Game.UI._old.Base.Model;
using JetBrains.Annotations;

namespace Game.UI._old.Menu.State
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
            // var stateBaseData = new UISubStateBaseData(UIManager, GameStateType.Menu);

            // RegisterSubState(MenuStateType.Main, new MenuStateMain(stateBaseData, MenuStateType.Main));
            // RegisterSubState(MenuStateType.Settings, new MenuStateSettings(stateBaseData, MenuStateType.Settings));
            // RegisterSubState(MenuStateType.AudioSettings,
                // new MenuStateSettingsAudio(stateBaseData, MenuStateType.AudioSettings));
            // RegisterSubState(MenuStateType.VideoSettings,
                // new MenuStateSettingsVideo(stateBaseData, MenuStateType.VideoSettings));
        }

        protected override void OnEnter()
        {
            throw new NotImplementedException();
        }

        protected override void OnExit()
        {
            throw new NotImplementedException();
        }

        protected override void Subscribe()
        {
            throw new NotImplementedException();
        }
    }
}
