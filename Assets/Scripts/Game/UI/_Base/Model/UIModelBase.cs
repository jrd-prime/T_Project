using Core.FSM.Interfaces;
using Core.Managers.Game;
using Core.Managers.UI;
using R3;
using Zenject;

namespace Game.UI._Base.Model
{
    public abstract class UIModelBase
    {
        protected IUIManager UIManager { get; private set; }
        protected DiContainer Container { get; private set; }
        protected IGameManager GameManager { get; private set; }
        protected IGameStateDispatcher _ra { get; private set; }

        protected readonly CompositeDisposable Disposables = new();

        [Inject]
        private void Construct(DiContainer container)
        {
            Container = container;
            UIManager = Container.Resolve<IUIManager>();
            GameManager = Container.Resolve<IGameManager>();
            _ra = Container.Resolve<IGameStateDispatcher>();
        }
    }
}
