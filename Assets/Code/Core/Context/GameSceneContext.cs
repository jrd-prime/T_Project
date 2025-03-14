using System;
using Code.Core.Managers.Camera._Game._Scripts.Framework.Manager.JCamera;
using Code.Core.Managers.Game;
using Code.Core.Systems;
using Code.Core.WORK.JStateMachine;
using Code.Core.WORK.UI.GameOver;
using Code.Core.WORK.UI.Pause;
using Code.Core.WORK.UI.Win;
using Code.Core.WORK.UIManager;
using Code.Core.WORK.UIStates;
using Code.Core.WORK.UIStates._Base.Model;
using Code.Core.WORK.UIStates.Gameplay;
using Code.Core.WORK.UIStates.Gameplay.State;
using Code.Core.WORK.UIStates.Menu;
using Code.Core.WORK.UIStates.Menu.State;
using Code.Core.WORK.UIStates.Win;
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
                .As<IGameManager, IInitializable>();

            if (!uiManagerPrefab) throw new NullReferenceException("UIManager is null. " + name);
            builder.RegisterComponentInNewPrefab<UIManager>(uiManagerPrefab, Lifetime.Singleton)
                .As<IUIManager, IInitializable>();

            builder.Register<IJStateMachine, JStateMachine>(Lifetime.Singleton).As<IInitializable>();
            InitializeUIModelsAndViewModels(builder);

            builder.Register<CameraFollowSystem>(Lifetime.Singleton)
                .AsSelf();

            builder.Register<MenuState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GameplayState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            // builder.Register<PauseMenuState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            // builder.Register<GameOverState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<WinState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            builder.Register<IHeroModel, HeroModel>(Lifetime.Singleton)
                .As<IInitializable, IFixedTickable>();
        }

        private static void InitializeUIModelsAndViewModels(IContainerBuilder builder)
        {
            // // Menu
            // builder.Register<MenuUIModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            // builder.Register<MenuUIViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            //
            // // Gameplay
            // builder.Register<GameplayUIModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            // builder.Register<GameplayUIViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();


            // State models
            builder.Register<IMenuModel, MenuModel>(Lifetime.Singleton).As<IInitializable>();
            builder.Register<IGameplayModel, GameplayModel>(Lifetime.Singleton).As<IInitializable>();
            builder.Register<IGameoverModel, GameoverModel>(Lifetime.Singleton).As<IInitializable>();
            builder.Register<IPauseModel, PauseModel>(Lifetime.Singleton).As<IInitializable>();
            builder.Register<IWinModel, WinModel>(Lifetime.Singleton).As<IInitializable>();

            // View models 
            builder.Register<IMenuViewModel, MenuViewModel>(Lifetime.Singleton).As<IInitializable, IDisposable>();
            builder.Register<IGameplayViewModel, GameplayViewModel>(Lifetime.Singleton)
                .As<IInitializable, IDisposable>();
        }
    }
}
