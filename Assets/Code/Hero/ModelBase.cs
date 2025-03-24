using Code.Core.Providers;
using Code.Extensions;
using R3;
using Zenject;

namespace Code.Hero
{
    public abstract class ModelBase
    {
        protected DiContainer Container { get; private set; }
        protected ISettingsProvider SettingsProvider { get; private set; }

        protected readonly CompositeDisposable Disposables = new();

        [Inject]
        private void Construct(DiContainer container)
        {
            Container = container;
            SettingsProvider = Container.ResolveAndCheckOnNull<ISettingsProvider>();
        }
    }
}
