using Code.Core.JStateMachine;
using Code.Core.Managers.Game;
using Code.Core.UI._Base.Model;
using JetBrains.Annotations;
using R3;
using VContainer;

namespace Code.Core.UI.Views.Menu
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
