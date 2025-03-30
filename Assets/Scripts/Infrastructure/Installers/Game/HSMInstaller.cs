using Core.HSM;
using Core.HSM.States;
using Core.HSM.States.Gameplay;
using Core.HSM.States.Menu;
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
