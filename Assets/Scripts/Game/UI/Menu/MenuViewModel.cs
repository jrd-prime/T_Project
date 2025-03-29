using Core.FSM.Data;
using Game.UI._Base.Model;
using Game.UI._Base.ViewModel;
using Game.UI.Gameplay.State;
using Game.UI.Menu.State;
using R3;
using Tools;
using UnityEngine;

namespace Game.UI.Menu
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

    public class MenuViewModel : UIViewModelBase<IMenuModel, MenuStateType>, IMenuViewModel
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
            ExitButtonClicked.Subscribe(ExitButtonClickedHandler).AddTo(Disposables);

            AudioButtonClicked.Subscribe(AudioButtonClickedHandler).AddTo(Disposables);
            VideoButtonClicked.Subscribe(VideoButtonClickedHandler).AddTo(Disposables);

            BackButtonClicked.Subscribe(BackButtonClickedHandler).AddTo(Disposables); // TODO
        }

        private void BackButtonClickedHandler(Unit _) =>
            Model.GameStateChangeRequest(new StateDataVo(GameStateType.Menu, MenuStateType.Main));

        private void AudioButtonClickedHandler(Unit _)
        {
            Debug.LogWarning("AudioButtonClickedHandler. Not implemented yet.");
            // return;
            Model.GameStateChangeRequest(new StateDataVo(GameStateType.Menu, MenuStateType.AudioSettings));
        }

        private void VideoButtonClickedHandler(Unit _)
        {
            Debug.LogWarning("VideoButtonClickedHandler. Not implemented yet.");
            // return;
            Model.GameStateChangeRequest(new StateDataVo(GameStateType.Menu, MenuStateType.VideoSettings));
        }

        private void StartButtonClickedHandler(Unit _) =>
            Model.GameStateChangeRequest(new StateDataVo(GameStateType.Gameplay, GameplayStateType.Main));

        private void SettingsButtonClickedHandler(Unit _) =>
            Model.GameStateChangeRequest(new StateDataVo(GameStateType.Menu, MenuStateType.Settings));

        private void ExitButtonClickedHandler(Unit _)
        {
            Model.GameStateChangeRequest(new StateDataVo(GameStateType.Exit)); // TODO
            ExitHelp.ExitGame();
        }
    }
}
