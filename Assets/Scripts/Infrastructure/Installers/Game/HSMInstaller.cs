using Core.HSM;
using Core.HSM.States;
using Core.HSM.States.Gameplay;
using Core.HSM.States.Menu;
using Zenject;

namespace Infrastructure.Installers
{
    public sealed class HSMInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<RootState>().AsSingle().NonLazy();
            Container.Bind<GameplayState>().AsSingle().NonLazy();
            Container.Bind<MenuState>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<HSM>().AsSingle();
        }
    }
}
