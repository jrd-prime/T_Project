using Core.Managers.HSM.Impls.States.Gameplay;
using Game.UI.Common;
using Game.UI.Data;
using Game.UI.Interfaces.Model;
using Tools;

namespace Game.UI.Impls.Menu
{
    public interface IMenuViewModel : IUIViewModel
    {
        void StartBtnClicked();
        void SettingsBtnClicked();
        void BackBtnClicked();
        void ExitBtnClicked();
        void AudioBtnClicked();
        void VideoBtnClicked();
        void ContinueBtnClicked();
        void MenuBtnClicked();
    }

    public sealed class MenuViewModel : UIViewModelBase<IMenuModel>, IMenuViewModel
    {
        public override void Initialize()
        {
        }

        protected override ViewRegistryType GetRegistryType() => ViewRegistryType.Menu;

        // State
        public void StartBtnClicked() => ChangeGameStateTo(typeof(GameplayState));
        public void ContinueBtnClicked() => ChangeGameStateTo(typeof(GameplayState));

        // Local
        public void SettingsBtnClicked() => SwitchLocalViewTo("settings");
        public void AudioBtnClicked() => SwitchLocalViewTo("settings-audio");
        public void VideoBtnClicked() => SwitchLocalViewTo("settings-video");
        public void MenuBtnClicked() => SwitchLocalViewTo("main");

        // Global
        public void BackBtnClicked() => SwitchToPreviousView();
        public void ExitBtnClicked() => ExitHelp.ExitGame();
    }
}
