using Code.Core.FSM;
using Code.Core.UI._Base.Data;
using Code.Core.UI._Base.Model;
using Code.Core.UI._Base.ViewModel;
using Code.Core.UI.Menu.State;
using Code.Tools;
using R3;

namespace Code.Core.UI.Menu
{
    public interface IMenuViewModel : IUIViewModel
    {
        public Subject<Unit> BackButtonClicked { get; }
        public Subject<Unit> StartButtonClicked { get; }
        public Subject<Unit> SettingsButtonClicked { get; }
        public Subject<Unit> ExitButtonClicked { get; }
    }

    public class MenuViewModel : UIViewModelBase<IMenuModel, MenuStateType>, IMenuViewModel
    {
        public Subject<Unit> BackButtonClicked { get; } = new();
        public Subject<Unit> StartButtonClicked { get; } = new();
        public Subject<Unit> SettingsButtonClicked { get; } = new();
        public Subject<Unit> ExitButtonClicked { get; } = new();

        public override void Initialize()
        {
            StartButtonClicked.Subscribe(StartButtonClickedHandler).AddTo(Disposables);
            SettingsButtonClicked.Subscribe(SettingsButtonClickedHandler).AddTo(Disposables);
            ExitButtonClicked.Subscribe(ExitButtonClickedHandler).AddTo(Disposables);

            BackButtonClicked
                .Subscribe(_ => Model.GameStateChangeRequest(new StateData(GameStateType.Menu))) // TODO
                .AddTo(Disposables);
        }

        private void StartButtonClickedHandler(Unit _) =>
            Model.GameStateChangeRequest(new StateData(GameStateType.Gameplay));

        private void SettingsButtonClickedHandler(Unit _) =>
            Model.GameStateChangeRequest(new StateData(GameStateType.Menu, MenuStateType.Settings));

        private void ExitButtonClickedHandler(Unit _)
        {
            Model.GameStateChangeRequest(new StateData(GameStateType.Exit)); // TODO
            ExitHelp.ExitGame();
        }
    }
}
