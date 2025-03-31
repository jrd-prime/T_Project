using Core.Managers.Game.Interfaces;
using Core.Managers.UI.Interfaces;
using R3;
using Zenject;

namespace Game.UI.Common.Base.Model
{
    public abstract class UIModelBase
    {
        protected IUIManager UIManager { get; private set; }
        protected DiContainer Container { get; private set; }
        protected IGameManager GameManager { get; private set; }

        protected readonly CompositeDisposable Disposables = new();

        [Inject]
        private void Construct(DiContainer container)
        {
            Container = container;
            UIManager = Container.Resolve<IUIManager>();
            GameManager = Container.Resolve<IGameManager>();
        }
    }
}
