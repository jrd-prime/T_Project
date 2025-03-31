using Core.HSM;
using Zenject;

namespace Infrastructure.Installers.Game
{
    public sealed class HSMInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<HSM>().AsSingle().NonLazy();
        }
    }
}
