using Code.Core.FSM;
using Code.Core.Managers.Game;
using Code.Core.Managers.UI;
using R3;
using Zenject;

namespace Code.UI._Base.Model
{
    public abstract class UIModelBase
    {
        protected IUIManager UIManager { get; private set; }
        protected DiContainer Container { get; private set; }
        protected IGameManager GameManager { get; private set; }
        protected IStateMachineReactiveAdapter _ra { get; private set; }

        protected readonly CompositeDisposable Disposables = new();

        [Inject]
        private void Construct(DiContainer container)
        {
            Container = container;
            UIManager = Container.Resolve<IUIManager>();
            GameManager = Container.Resolve<IGameManager>();
            _ra = Container.Resolve<IStateMachineReactiveAdapter>();
        }
    }
}
