using Code.Core.UIOLD._Base.ViewModel;
using R3;
using VContainer.Unity;

namespace Code.Core.UIOLD.Views.Menu
{
    internal class MenuUIViewModel : CustomUIViewModel<IMenuUIModelController>, IInitializable
    {
        public ReactiveCommand StartButtonClickCommand { get; private set; }

        public void Initialize()
        {
            StartButtonClickCommand = new ReactiveCommand();

            StartButtonClickCommand.Subscribe(_ => Model.NotifyButtonClicked()).AddTo(Disposables);
        }
    }
}
