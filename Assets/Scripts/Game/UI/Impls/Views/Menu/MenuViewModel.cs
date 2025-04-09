using Core.Managers.HSM.Impls.States.Gameplay;
using Game.UI.Common;
using Game.UI.Data;
using Game.UI.Interfaces.Model;
using Tools;

namespace Game.UI.Impls.Views.Menu
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
        public void StartBtnClicked()
        {
            //TODO выглядит стремно
            ChangeGameStateTo(typeof(GameplayState));
            GameManager.StartNewGame();
        }

        public void ContinueBtnClicked()
        {
            ChangeGameStateTo(typeof(GameplayState));
            GameManager.ContinueGame();
        }

        // Local
        public void SettingsBtnClicked() => ShowLocalView(ViewIDConst.Settings);
        public void AudioBtnClicked() => ShowLocalView(ViewIDConst.SettingsAudio);
        public void VideoBtnClicked() => ShowLocalView(ViewIDConst.SettingsVideo);
        public void MenuBtnClicked() => ShowLocalView(ViewIDConst.Main);

        // Global
        public void BackBtnClicked() => SwitchToPreviousView();
        public void ExitBtnClicked() => ExitHelp.ExitGame();
    }
}
