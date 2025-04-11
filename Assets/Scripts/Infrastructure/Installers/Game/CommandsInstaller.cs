using Game.Anima;
using Zenject;

namespace Infrastructure.Installers.Game
{
    public sealed class CommandsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GatherCommand>().AsSingle().NonLazy();
        }
    }
}
