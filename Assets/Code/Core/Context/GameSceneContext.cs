using System;
using Code.Core.Managers.Camera._Game._Scripts.Framework.Manager.JCamera;
using Code.Core.Managers.Game;
using Code.Core.Managers.UI;
using Code.Core.Systems;
using Code.Core.UI.Views.Gameplay;
using Code.Core.UI.Views.Menu;
using Code.Hero;
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

            if (!cameraManager) throw new NullReferenceException("CameraManager is null. " + name);
            builder.RegisterComponent(cameraManager)
                .As<ICameraManager, IInitializable>();

            if (!gameManagerPrefab) throw new NullReferenceException("GameManager is null. " + name);
            builder.RegisterComponentInNewPrefab<GameManager>(gameManagerPrefab, Lifetime.Singleton)
                .AsSelf().As<IInitializable>();

            if (!uiManagerPrefab) throw new NullReferenceException("UIManager is null. " + name);
            builder.RegisterComponentInNewPrefab<UIManager>(uiManagerPrefab, Lifetime.Singleton)
                .As<IUIManager, IInitializable>();

            InitializeUIModelsAndViewModels(builder);

            builder.Register<CameraFollowSystem>(Lifetime.Singleton)
                .AsSelf();

            builder.Register<IHeroModel, HeroModel>(Lifetime.Singleton)
                .As<IInitializable, IFixedTickable>();
        }

        private static void InitializeUIModelsAndViewModels(IContainerBuilder builder)
        {
            // Menu
            builder.Register<MenuUIModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<MenuUIViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            // Gameplay
            builder.Register<GameplayUIModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GameplayUIViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }
    }
}
