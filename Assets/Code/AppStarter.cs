using System;
using Code.Core.Bootstrap;
using Code.Core.Providers;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
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

            bootstrapLoader.AddForBootstrapInitialization(settingsProvider);

            Debug.Log("Starting Services initialization...");
            await bootstrapLoader.StartServicesInitializationAsync();
            Debug.Log("End Services initialization...");

            // var gameScene = await _assetProvider.LoadSceneAsync(AssetsConst.GameScene, LoadSceneMode.Single);

            // TODO FadeOut loading screen view 
            // SceneManager.SetActiveScene(gameScene.Scene);

            Debug.LogWarning("<color=green><b>=== APP STARTED! ===</b></color>");
        }
    }
}
