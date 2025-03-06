using System;
using Code.Core.Bootstrap;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Core.Context
{
    public sealed class BootstrapContext : LifetimeScope
    {
        [SerializeField] private BootstrapUIView bootstrapUIView;

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("<color=cyan>Bootstrap context</color>");

            builder.Register<IBootstrapUIModel, BootstrapUIModel>(Lifetime.Singleton);
            builder.Register<IBootstrapUIViewModel, BootstrapUIViewModel>(Lifetime.Singleton);

            if (!bootstrapUIView) throw new NullReferenceException("BootstrapUIView is null.");

            builder.RegisterComponent(bootstrapUIView).AsSelf();

            builder.Register<BootstrapLoader>(Lifetime.Singleton).AsSelf().As<IInitializable>();

            builder.RegisterEntryPoint<AppStarter>();
        }
    }
}
