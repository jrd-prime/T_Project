using UnityEngine;
using Zenject;

namespace Bootstrap
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.LogWarning("BootstrapInstaller.InstallBindings");
            Container.Bind<IBootstrapUIModel>().To<BootstrapUIModel>().AsSingle();
            Container.Bind<IBootstrapUIViewModel>().To<BootstrapUIViewModel>().AsSingle();
        }
    }
}
