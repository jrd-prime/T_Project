using System;
using Core.Character.Hero;
using Core.Managers.Camera._Game._Scripts.Framework.Manager.JCamera;
using Core.Managers.Game;
using Core.Managers.Game.Impls;
using Core.Managers.UI.Impls;
using Game.Systems;
using Game.UI.Impls.Gameplay.Gameplay;
using Game.UI.Impls.Menu;
using Game.UI.Impls.Pause;
using ModestTree;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Infrastructure.Installers.Game
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private CameraManager cameraManager;
        [SerializeField] private UIManager uiManagerPrefab;
        [FormerlySerializedAs("aGameManagerPrefab")] [SerializeField] private GameManager gameManagerPrefab;

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
        }

        private void InitializeManagers(DiContainer container)
        {
            RegisterComponent(container, cameraManager, "CameraManager");
            RegisterPrefabComponent<GameManager>(container, gameManagerPrefab, "GameManager");
            RegisterPrefabComponent<UIManager>(container, uiManagerPrefab, "UIManager");
        }

        private static void InitializeViewStates(DiContainer container)
        {
            // container.BindInterfacesAndSelfTo<NewMenuState>().AsSingle();
            // container.BindInterfacesAndSelfTo<NewGameplayState>().AsSingle();
            // container.BindInterfacesAndSelfTo<PauseState>().AsSingle();
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
