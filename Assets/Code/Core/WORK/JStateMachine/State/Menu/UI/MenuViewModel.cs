using Code.Core.WORK.Enums.States;
using Code.Core.WORK.JStateMachine.Data;
using Code.Core.WORK.JStateMachine.State.Menu.UI.Base;
using Code.Core.WORK.UI.Base.Model;
using Code.Core.WORK.UI.Base.ViewModel;
using Code.Tools;
using R3;

namespace Code.Core.WORK.JStateMachine.State.Menu.UI
{
    public class MenuViewModel : UIViewModelBase<IMenuModel, EMenuSubState>, IMenuViewModel
    {
        public Subject<Unit> BackButtonClicked { get; } = new();
        public Subject<Unit> PlayButtonClicked { get; } = new();
        public Subject<Unit> SettingsButtonClicked { get; } = new();
        public Subject<Unit> ExitButtonClicked { get; } = new();

        public override void Initialize()
        {
            PlayButtonClicked
                .Subscribe(_ => Model.SetGameState(new StateData(EGameState.Gameplay)))
                .AddTo(Disposables);

            SettingsButtonClicked
                .Subscribe(_ => Model.SetGameState(new StateData(EGameState.Menu, EMenuSubState.Settings)))
                .AddTo(Disposables);

            ExitButtonClicked
                .Subscribe(_ =>
                {
                    Model.SetGameState(new StateData(EGameState.Exit));
                    ExitHelp.ExitGame();
                })
                .AddTo(Disposables);

            BackButtonClicked
                .Subscribe(_ => Model.SetGameState(new StateData(EGameState.Menu)))
                .AddTo(Disposables);
        }
    }
}
