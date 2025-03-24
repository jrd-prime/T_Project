using Code.Core.FSM;
using Code.UI._Base.Data;
using Code.UI._Base.ViewModel;
using Code.UI.Gameplay.State;
using Code.UI.Menu.State;
using Code.UI.Pause.State;
using R3;

namespace Code.UI.Pause
{
    public interface IPauseViewModel : IUIViewModel
    {
        public Subject<Unit> ContinueButtonClicked { get; }
        public Subject<Unit> MenuButtonClicked { get; }
    }

    public sealed class PauseViewModel : UIViewModelBase<IPauseModel, PauseStateType>, IPauseViewModel
    {
        public Subject<Unit> ContinueButtonClicked { get; } = new();
        public Subject<Unit> MenuButtonClicked { get; } = new();

        public override void Initialize()
        {
            ContinueButtonClicked.Subscribe(ContinueButtonClickedHandler).AddTo(Disposables);
            MenuButtonClicked.Subscribe(MenuButtonClickedHandler).AddTo(Disposables);
        }

        private void ContinueButtonClickedHandler(Unit _) =>
            Model.GameStateChangeRequest(new StateData(GameStateType.Gameplay, GameplayStateType.Main));

        private void MenuButtonClickedHandler(Unit _) =>
            Model.GameStateChangeRequest(new StateData(GameStateType.Menu, MenuStateType.Main));
    }
}
