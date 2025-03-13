using Code.Core.Providers;
using Code.Extensions;
using R3;
using VContainer;

namespace Code.Hero
{
    public abstract class ModelBase
    {
        protected IObjectResolver Resolver { get; private set; }
        protected ISettingsProvider SettingsProvider { get; private set; }

        protected readonly CompositeDisposable Disposables = new();

        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            Resolver = resolver;
            SettingsProvider = Resolver.ResolveAndCheckOnNull<ISettingsProvider>();
        }
    }
}
