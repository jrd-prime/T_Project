using Core.Managers.HSM.Impls.States.Gameplay;
using Core.Managers.HSM.Signals;
using Core.Managers.UI.Signals;
using Game.UI.Common.Base.Model;
using Game.UI.Common.Base.ViewModel;
using Game.UI.Data;
using R3;
using Tools;

namespace Game.UI.Impls.Menu
{
    public interface IMenuViewModel : IUIViewModel
    {
        public Subject<Unit> BackButtonClicked { get; }
        public Subject<Unit> StartButtonClicked { get; }
        public Subject<Unit> SettingsButtonClicked { get; }
        public Subject<Unit> ExitButtonClicked { get; }
        public Subject<Unit> AudioButtonClicked { get; }
        public Subject<Unit> VideoButtonClicked { get; }
    }

    public class MenuViewModel : UIViewModelBase<IMenuModel>, IMenuViewModel
    {
        public Subject<Unit> BackButtonClicked { get; } = new();
        public Subject<Unit> StartButtonClicked { get; } = new();
        public Subject<Unit> SettingsButtonClicked { get; } = new();
        public Subject<Unit> ExitButtonClicked { get; } = new();
        public Subject<Unit> AudioButtonClicked { get; } = new();
        public Subject<Unit> VideoButtonClicked { get; } = new();

        public override void Initialize()
        {
            StartButtonClicked.Subscribe(StartButtonClickedHandler).AddTo(Disposables);
            SettingsButtonClicked.Subscribe(SettingsButtonClickedHandler).AddTo(Disposables);
            // ExitButtonClicked.Subscribe(ExitButtonClickedHandler).AddTo(Disposables);
            //
            // AudioButtonClicked.Subscribe(AudioButtonClickedHandler).AddTo(Disposables);
            // VideoButtonClicked.Subscribe(VideoButtonClickedHandler).AddTo(Disposables);
            //
            BackButtonClicked.Subscribe(BackButtonClickedHandler).AddTo(Disposables); // TODO
        }

        private void BackButtonClickedHandler(Unit _) => SignalBus.Fire(new SwitchToPreviousViewSignalVo());

        // private void AudioButtonClickedHandler(Unit _)
        // {
        //     Debug.LogWarning("AudioButtonClickedHandler. Not implemented yet.");
        //     // return;
        //     Model.GameStateChangeRequest(new StateDataVo(GameStateType.Menu, MenuStateType.AudioSettings));
        // }
        //
        // private void VideoButtonClickedHandler(Unit _)
        // {
        //     Debug.LogWarning("VideoButtonClickedHandler. Not implemented yet.");
        //     // return;
        //     Model.GameStateChangeRequest(new StateDataVo(GameStateType.Menu, MenuStateType.VideoSettings));
        // }
        //
        private void StartButtonClickedHandler(Unit _) =>
            SignalBus.Fire(new ChangeGameStateSignalVo(typeof(GameplayState)));


        private void SettingsButtonClickedHandler(Unit _) =>
            SignalBus.Fire(new SwitchLocalViewSignalVo(ViewRegistryType.Menu, "settings"));

        private void ExitButtonClickedHandler(Unit _)
        {
            ExitHelp.ExitGame();
        }
    }
}
