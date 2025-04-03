using Core.Managers.HSM.Impls.States.Gameplay;
using Core.Managers.UI.Impls;
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
        public void SettingsBtnClicked() => ShowLocalView(ViewIDConst.Settings);


        public void AudioBtnClicked() => ShowLocalView("settings-audio");
        public void VideoBtnClicked() => ShowLocalView("settings-video");
        public void MenuBtnClicked() => ShowLocalView("main");

        // Global
        public void BackBtnClicked() => SwitchToPreviousView();
        public void ExitBtnClicked() => ExitHelp.ExitGame();
    }
}
