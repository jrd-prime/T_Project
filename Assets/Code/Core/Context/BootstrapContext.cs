using System;
using Code.Core.Bootstrap;
using Code.Core.JStateMachineOLD;
using Code.Core.JStateMachineOLD.State;
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

            RegisterBootstrapServices(builder);
            RegisterStateMachineAndStates(builder);

            builder.Register<IGameStateMachine, StateMachine>(Lifetime.Singleton).As<IInitializable>();
            builder.RegisterEntryPoint<AppStarter>();
        }

        private void RegisterBootstrapServices(IContainerBuilder builder)
        {
            builder.Register<IBootstrapUIModel, BootstrapUIModel>(Lifetime.Singleton);
            builder.Register<IBootstrapUIViewModel, BootstrapUIViewModel>(Lifetime.Singleton);

            if (!bootstrapUIView)
                throw new NullReferenceException("BootstrapUIView is null. " + nameof(BootstrapContext));
            builder.RegisterComponent(bootstrapUIView).AsSelf();

            builder.Register<BootstrapLoader>(Lifetime.Singleton).AsSelf().As<IInitializable>();
        }

        private static void RegisterStateMachineAndStates(IContainerBuilder builder)
        {
            builder.Register<IGameState, MenuState>(Lifetime.Singleton).AsSelf();
            builder.Register<IGameState, GamePlayState>(Lifetime.Singleton).AsSelf();
            builder.Register<IGameState, PauseMenuState>(Lifetime.Singleton).AsSelf();
            builder.Register<IGameState, GameOverState>(Lifetime.Singleton).AsSelf();
            builder.Register<IGameState, WinState>(Lifetime.Singleton).AsSelf();
        }
    }
}
