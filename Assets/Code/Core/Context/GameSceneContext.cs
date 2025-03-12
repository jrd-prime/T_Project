using System;
using Code.Core.Managers.Camera._Game._Scripts.Framework.Manager.JCamera;
using Code.Core.Managers.Game;
using Code.Core.Managers.UI;
using Code.Core.Systems;
using Code.Hero;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Core.Context
{
    public class GameSceneContext : LifetimeScope
    {
        [SerializeField] private CameraManager cameraManager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private GameManager gameManager;

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.LogWarning("<color=cyan>GameScene context</color>");

            if (!cameraManager) throw new NullReferenceException("CameraManager is null. " + name);
            builder.RegisterComponent(cameraManager).As<ICameraManager, IInitializable>();

            if (!gameManager) throw new NullReferenceException("GameManager is null. " + name);
            builder.RegisterComponent(gameManager).AsSelf();

            if (!uiManager) throw new NullReferenceException("UIManager is null. " + name);
            // builder.RegisterComponent(uiManager).As<IUIManager, IInitializable>();

            builder.Register<CameraFollowSystem>(Lifetime.Singleton).AsSelf();

            builder.Register<IHeroModel, HeroModel>(Lifetime.Singleton).As<IInitializable, IFixedTickable>();
        }
    }
}
