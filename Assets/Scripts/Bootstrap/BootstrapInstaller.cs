using System;
using UnityEngine;
using Zenject;

namespace Bootstrap
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private BootstrapUIView bootstrapUIView;

        public override void InstallBindings()
        {
            Debug.Log("<color=cyan>BootstrapInstaller</color>");

            Container.Bind<IBootstrapUIModel>().To<BootstrapUIModel>().AsSingle();

            Container.Bind<IBootstrapUIViewModel>().To<BootstrapUIViewModel>().AsSingle();

            if (bootstrapUIView == null)
                throw new NullReferenceException("BootstrapUIView is null. " + nameof(BootstrapInstaller));
            Container.Bind<BootstrapUIView>()
                .FromComponentInNewPrefab(bootstrapUIView)
                .AsSingle()
                .NonLazy();


            Container.Bind(typeof(BootstrapLoader), typeof(IInitializable))
                .To<BootstrapLoader>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesTo<AppStarter>().AsSingle().NonLazy();
        }
    }
}
