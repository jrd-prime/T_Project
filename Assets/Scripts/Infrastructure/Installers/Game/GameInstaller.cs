using System;
using Core.Character.Hero;
using Core.Managers.Camera._Game._Scripts.Framework.Manager.JCamera;
using Core.Managers.Game;
using Core.Managers.Game.Impls;
using Core.Managers.UI.Impls;
using Game.Systems;
using Game.UI.Impls.Gameplay.Gameplay;
using Game.UI.Impls.Menu;
using Infrastructure.Input.Handlers;
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
            if (!_mainEmpty) throw new NullReferenceException("Main empty game object is not found. (--- Default)");

            InitializeManagers();

            Container.BindInterfacesAndSelfTo<GameManagerRequestHandler>().AsSingle();


            InitializeUIModelsAndViewModels();
            InitializeViewStates();

            Container.Bind<CameraFollowSystem>().AsSingle();

            Container.BindInterfacesTo<HeroModel>().AsSingle().NonLazy();

            BindKeyHandlers();
        }

        private void BindKeyHandlers()
        {
            Container.Bind<EscapeKeyHandler>().AsSingle().NonLazy();
            Container.Bind<InventoryKeyHandler>().AsSingle().NonLazy();
        }

        private void InitializeManagers()
        {
            RegisterComponent(cameraManager, "CameraManager");
            RegisterPrefabComponent<GameManager>(gameManagerPrefab, "GameManager");
            RegisterPrefabComponent<UIManager>(uiManagerPrefab, "UIManager");
        }

        private void InitializeViewStates()
        {
            // container.BindInterfacesAndSelfTo<NewMenuState>().AsSingle();
            // container.BindInterfacesAndSelfTo<NewGameplayState>().AsSingle();
            // container.BindInterfacesAndSelfTo<PauseState>().AsSingle();
        }

        private void InitializeUIModelsAndViewModels()
        {
            Container.BindInterfacesAndSelfTo<MenuModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<MenuViewModel>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameplayModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayViewModel>().AsSingle();
        }

        private void RegisterComponent<T>(T component, string componentName) where T : class
        {
            if (component == null)
                throw new NullReferenceException($"{componentName} is null in {gameObject.name}");

            Container.BindInterfacesAndSelfTo<T>()
                .FromInstance(component)
                .AsSingle();
        }

        private void RegisterPrefabComponent<T>(T prefabComponent, string componentName)
            where T : MonoBehaviour
        {
            if (prefabComponent == null)
                throw new NullReferenceException($"{componentName} is null in {gameObject.name}");

            Container.BindInterfacesAndSelfTo<T>()
                .FromComponentInNewPrefab(prefabComponent.gameObject) // Извлекаем GameObject из MonoBehaviour
                .AsSingle().OnInstantiated<T>((ctx, vacuumContainer) =>
                {
                    vacuumContainer.transform.parent = _mainEmpty.transform;
                });
        }
    }
}
