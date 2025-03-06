using System;
using Code.Core.Bootstrap;
using Code.Core.Data;
using Code.Core.Providers;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Code
{
    [UsedImplicitly]
    public sealed class AppStarter : IInitializable
    {
        private IObjectResolver _resolver;

        [Inject]
        private void Construct(IObjectResolver resolver) => _resolver = resolver;

        public void Initialize() => InitializeAsync().Forget();

        private async UniTask InitializeAsync()
        {
            var bootstrapLoader = _resolver.Resolve<BootstrapLoader>() ??
                                  throw new NullReferenceException("BootstrapLoader is null.");

            var settingsProvider = _resolver.Resolve<ISettingsProvider>() ??
                                   throw new NullReferenceException("SettingsProvider is null.");
            var assetProvider = _resolver.Resolve<IAssetProvider>() ??
                                throw new NullReferenceException("AssetProvider is null.");

            bootstrapLoader.AddForBootstrapInitialization(settingsProvider);
            bootstrapLoader.AddForBootstrapInitialization(assetProvider);

            var bootTask = bootstrapLoader.StartServicesInitializationAsync();
            var sceneTask = assetProvider.LoadSceneAsync(AssetNameConst.GameScene, LoadSceneMode.Single);

            Debug.Log("Starting Services initialization...");
            await UniTask.WhenAll(bootTask, sceneTask);
            Debug.Log("End Services initialization...");

            var gameScene = sceneTask.GetAwaiter().GetResult();

            // TODO FadeOut loading screen view 
            SceneManager.SetActiveScene(gameScene.Scene);

            Debug.LogWarning("<color=green><b>=== APP STARTED! ===</b></color>");
        }
    }
}
