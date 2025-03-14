using Code.Core.FSM;
using Code.Core.UI._Base.Model;
using Code.Core.UI._Base.ViewModel;
using Code.Core.UI._Base.ViewStateTypes;
using Code.Tools;
using R3;

namespace Code.Core.UI.Menu
{
    public interface IMenuViewModel : IUIViewModel
    {
        public Subject<Unit> BackButtonClicked { get; }
        public Subject<Unit> PlayButtonClicked { get; }
        public Subject<Unit> SettingsButtonClicked { get; }
        public Subject<Unit> ExitButtonClicked { get; }
    }

    public class MenuViewModel : UIViewModelBase<IMenuModel, MenuStateType>, IMenuViewModel
    {
        public Subject<Unit> BackButtonClicked { get; } = new();
        public Subject<Unit> PlayButtonClicked { get; } = new();
        public Subject<Unit> SettingsButtonClicked { get; } = new();
        public Subject<Unit> ExitButtonClicked { get; } = new();

        public override void Initialize()
        {
            PlayButtonClicked
                .Subscribe(_ => Model.SetGameState(new StateData(GameStateType.Gameplay)))
                .AddTo(Disposables);

            SettingsButtonClicked
                .Subscribe(_ => Model.SetGameState(new StateData(GameStateType.Menu, MenuStateType.Settings)))
                .AddTo(Disposables);

            ExitButtonClicked
                .Subscribe(_ =>
                {
                    Model.SetGameState(new StateData(GameStateType.Exit));
                    ExitHelp.ExitGame();
                })
                .AddTo(Disposables);

            BackButtonClicked
                .Subscribe(_ => Model.SetGameState(new StateData(GameStateType.Menu)))
                .AddTo(Disposables);
        }
    }
}
