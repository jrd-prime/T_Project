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
using Code.UI.Pause;
using Code.UI.Pause.State;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Installers.Game
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private CameraManager cameraManager;
        [SerializeField] private UIManager uiManagerPrefab;
        [SerializeField] private GameManager gameManagerPrefab;

        public override void InstallBindings()
        {
            Log.Info("<color=cyan>GameInstaller</color>");
            InitializeManagers(Container);

            Container.BindInterfacesAndSelfTo<GameManagerRequestHandler>().AsSingle();


            InitializeUIModelsAndViewModels(Container);
            InitializeViewStates(Container);

            Container.Bind<CameraFollowSystem>().AsSingle();

            Container.BindInterfacesTo<HeroModel>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<JStateMachine>().AsSingle();
        }

        private void InitializeManagers(DiContainer container)
        {
            RegisterComponent(container, cameraManager, "CameraManager");
            RegisterPrefabComponent<GameManager>(container, gameManagerPrefab, "GameManager");
            RegisterPrefabComponent<UIManager>(container, uiManagerPrefab, "UIManager");
        }

        private static void InitializeViewStates(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<MenuState>().AsSingle();
            container.BindInterfacesAndSelfTo<GameplayState>().AsSingle();
            container.BindInterfacesAndSelfTo<PauseState>().AsSingle();
        }

        private static void InitializeUIModelsAndViewModels(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<MenuModel>().AsSingle();
            container.BindInterfacesAndSelfTo<MenuViewModel>().AsSingle();

            container.BindInterfacesAndSelfTo<GameplayModel>().AsSingle();
            container.BindInterfacesAndSelfTo<GameplayViewModel>().AsSingle();

            container.BindInterfacesAndSelfTo<PauseModel>().AsSingle();
            container.BindInterfacesAndSelfTo<PauseViewModel>().AsSingle();
        }

        private void RegisterComponent<T>(DiContainer container, T component, string componentName) where T : class
        {
            if (component == null)
                throw new NullReferenceException($"{componentName} is null in {gameObject.name}");

            container.BindInterfacesAndSelfTo<T>()
                .FromInstance(component)
                .AsCached();
        }

        private void RegisterPrefabComponent<T>(DiContainer container, T prefabComponent, string componentName)
            where T : MonoBehaviour
        {
            if (prefabComponent == null)
                throw new NullReferenceException($"{componentName} is null in {gameObject.name}");

            container.BindInterfacesAndSelfTo<T>()
                .FromComponentInNewPrefab(prefabComponent.gameObject) // Извлекаем GameObject из MonoBehaviour
                .AsSingle();
        }
    }
}
