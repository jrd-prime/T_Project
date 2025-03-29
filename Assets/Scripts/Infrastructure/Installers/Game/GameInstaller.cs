using System;
using Core.Character.Hero;
using Core.FSM;
using Core.Managers.Camera._Game._Scripts.Framework.Manager.JCamera;
using Core.Managers.Game;
using Core.Managers.UI;
using Game.Systems;
using Game.UI.Gameplay;
using Game.UI.Gameplay.State;
using Game.UI.Menu;
using Game.UI.Menu.State;
using Game.UI.Pause;
using Game.UI.Pause.State;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Game
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private CameraManager cameraManager;
        [SerializeField] private UIManager uiManagerPrefab;
        [SerializeField] private GameManager gameManagerPrefab;

        private GameObject _mainEmpty;

        public override void InstallBindings()
        {
            Log.Info("<color=cyan>GameInstaller</color>");

            _mainEmpty = GameObject.Find("--- MAIN");
            if (!_mainEmpty) throw new NullReferenceException("Main empty game object is not found. (--- MAIN)");

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
                .AsSingle().OnInstantiated<T>((ctx, vacuumContainer) =>
                {
                    vacuumContainer.transform.parent = _mainEmpty.transform;
                });
        }
    }
}
