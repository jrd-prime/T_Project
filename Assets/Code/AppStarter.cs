using Code.Core.Bootstrap;
using Code.Core.Providers;
using Code.Extensions;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

//TODO turn on input after app start
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
            var bootstrapLoader = _resolver.ResolveAndCheckOnNull<BootstrapLoader>();
            var bootstrapUIModel = _resolver.ResolveAndCheckOnNull<IBootstrapUIModel>();

            // Bootable services
            var settingsProvider = _resolver.ResolveAndCheckOnNull<ISettingsProvider>();
            var assetProvider = _resolver.ResolveAndCheckOnNull<IAssetProvider>();
            var firstSceneProvider = _resolver.ResolveAndCheckOnNull<FirstSceneProvider>();

            bootstrapLoader.AddForBootstrapInitialization(settingsProvider);
            bootstrapLoader.AddForBootstrapInitialization(assetProvider);
            bootstrapLoader.AddForBootstrapInitialization(firstSceneProvider);

            Debug.Log("Starting Services initialization...");
            await bootstrapLoader.StartServicesInitializationAsync();
            Debug.Log("End Services initialization...");

            await bootstrapUIModel.FadeOut(1f);

            var bootstrapScene = SceneManager.GetActiveScene();
            SceneManager.SetActiveScene(firstSceneProvider.FirstScene.Scene);
            SceneManager.UnloadSceneAsync(bootstrapScene);

            Debug.LogWarning("<color=green><b>=== APP STARTED! ===</b></color>");
        }
    }
}
