using Core.Extensions;
using Core.Providers;
using R3;
using Zenject;

namespace Core.Character.Hero
{
    public abstract class ModelBase
    {
        protected DiContainer Container { get; private set; }
        protected ISettingsProvider SettingsProvider { get; private set; }

        protected readonly CompositeDisposable Disposables = new();

        protected ModelBase(DiContainer container)
        {
            Container = container;
            SettingsProvider = Container.ResolveAndCheckOnNull<ISettingsProvider>();
        }
    }
}
