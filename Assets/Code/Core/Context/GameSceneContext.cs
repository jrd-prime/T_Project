using System;
using Code.Core.FSM;
using Code.Core.Managers.Camera._Game._Scripts.Framework.Manager.JCamera;
using Code.Core.Managers.Game;
using Code.Core.Managers.UI;
using Code.Core.Systems;
using Code.Hero;
using Code.UI._Base.Model;
using Code.UI.Gameplay;
using Code.UI.Gameplay.State;
using Code.UI.Menu;
using Code.UI.Menu.State;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Core.Context
{
    public class GameSceneContext : LifetimeScope
    {
        [SerializeField] private CameraManager cameraManager;
        [SerializeField] private UIManager uiManagerPrefab;
        [SerializeField] private GameManager gameManagerPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.LogWarning("<color=cyan>GameScene context</color>");

            InitializeManagers(builder);

            builder.Register<GameManagerRequestHandler>(Lifetime.Singleton).As<IInitializable>();

            builder.Register<IStateMachine, JStateMachine>(Lifetime.Singleton).As<IInitializable>();

            InitializeUIModelsAndViewModels(builder);
            InitializeViewStates(builder);

            builder.Register<CameraFollowSystem>(Lifetime.Singleton)
                .AsSelf();

            builder.Register<IHeroModel, HeroModel>(Lifetime.Singleton)
                .As<IInitializable, IFixedTickable>();
        }

        private void InitializeManagers(IContainerBuilder builder)
        {
            if (!cameraManager) throw new NullReferenceException("CameraManager is null. " + name);
            builder.RegisterComponent(cameraManager)
                .As<ICameraManager, IInitializable>();

            if (!gameManagerPrefab) throw new NullReferenceException("GameManager is null. " + name);
            builder.RegisterComponentInNewPrefab<GameManager>(gameManagerPrefab, Lifetime.Singleton)
                .As<IGameManager, IInitializable>();

            if (!uiManagerPrefab) throw new NullReferenceException("UIManager is null. " + name);
            builder.RegisterComponentInNewPrefab<UIManager>(uiManagerPrefab, Lifetime.Singleton)
                .As<IUIManager, IInitializable>();
        }

        private static void InitializeViewStates(IContainerBuilder builder)
        {
            builder.Register<MenuState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GameplayState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }

        private static void InitializeUIModelsAndViewModels(IContainerBuilder builder)
        {
            builder.Register<IMenuModel, MenuModel>(Lifetime.Singleton).As<IInitializable>();
            builder.Register<IMenuViewModel, MenuViewModel>(Lifetime.Singleton).As<IInitializable, IDisposable>();

            builder.Register<IGameplayModel, GameplayModel>(Lifetime.Singleton).As<IInitializable>();
            builder.Register<IGameplayViewModel, GameplayViewModel>(Lifetime.Singleton)
                .As<IInitializable, IDisposable>();
        }
    }
}
