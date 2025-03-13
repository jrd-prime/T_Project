using Code.Core.WORK.UI.Base.ViewModel;
using R3;

namespace Code.Core.WORK.JStateMachine.State.Menu.UI.Base
{
    public interface IMenuViewModel : IUIViewModel
    {
        public Subject<Unit> BackButtonClicked { get; }
        public Subject<Unit> PlayButtonClicked { get; }
        public Subject<Unit> SettingsButtonClicked { get; }
        public Subject<Unit> ExitButtonClicked { get; }
    }
}
