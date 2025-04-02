using Game.UI.Common;
using Game.UI.Data;
using Game.UI.Interfaces.Model;
using R3;

namespace Game.UI.Impls.Pause
{
    public interface IPauseViewModel : IUIViewModel
    {
        public Subject<Unit> ContinueButtonClicked { get; }
        public Subject<Unit> MenuButtonClicked { get; }
    }

    public sealed class PauseViewModel : UIViewModelBase<IPauseModel>, IPauseViewModel
    {
        public Subject<Unit> ContinueButtonClicked { get; } = new();
        public Subject<Unit> MenuButtonClicked { get; } = new();

        public override void Initialize()
        {
            // ContinueButtonClicked.Subscribe(ContinueButtonClickedHandler).AddTo(Disposables);
            // MenuButtonClicked.Subscribe(MenuButtonClickedHandler).AddTo(Disposables);
        }

        protected override ViewRegistryType GetRegistryType() => ViewRegistryType.Pause; 

        // private void ContinueButtonClickedHandler(Unit _) =>
        //     Model.GameStateChangeRequest(new StateDataVo(GameStateType.Gameplay, GameplayStateType.Main));

        // private void MenuButtonClickedHandler(Unit _) =>
        //     Model.GameStateChangeRequest(new StateDataVo(GameStateType.Menu, MenuStateType.Main));
    }
}
