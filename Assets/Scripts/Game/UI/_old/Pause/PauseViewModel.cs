using Core.FSM.Data;
using Game.UI._old.Base.ViewModel;
using Game.UI._old.Gameplay.State;
using Game.UI._old.Menu.State;
using Game.UI._old.Pause.State;
using R3;

namespace Game.UI._old.Pause
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
            Model.GameStateChangeRequest(new StateDataVo(GameStateType.Gameplay, GameplayStateType.Main));

        private void MenuButtonClickedHandler(Unit _) =>
            Model.GameStateChangeRequest(new StateDataVo(GameStateType.Menu, MenuStateType.Main));
    }
}
