using Code.Core.UIOLD._Base.Model;
using JetBrains.Annotations;
using R3;

namespace Code.Core.UIOLD.Views.Menu
{
    [UsedImplicitly]
    public class MenuUIModel : IMenuUIModel, IMenuUIModelController
    {
        private readonly Subject<Unit> _startButtonClicked = new();
        public Observable<Unit> OnStartButtonClicked => _startButtonClicked.AsObservable();


        public void NotifyButtonClicked() => _startButtonClicked.OnNext(Unit.Default);
    }

    internal interface IMenuUIModel : IUIModel
    {
        public Observable<Unit> OnStartButtonClicked { get; }
    }

    internal interface IMenuUIModelController : IUIModel
    {
        public void NotifyButtonClicked();
    }
}
