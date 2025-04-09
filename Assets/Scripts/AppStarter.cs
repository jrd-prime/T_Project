//TODO turn on input after app start

using Core;
using Cysharp.Threading.Tasks;
using Infrastructure.Assets;
using Infrastructure.Bootstrap;
using Infrastructure.Localization;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public sealed class AppStarter : IInitializable
{
    [Inject] private DiContainer _container;

    public void Initialize() => InitializeAsync().Forget();

    private async UniTask InitializeAsync()
    {
        var bootstrapLoader = _container.Resolve<BootstrapLoader>();
        var bootstrapUIModel = _container.Resolve<IBootstrapUIModel>();

        // Bootable services
        var settingsProvider = _container.Resolve<ISettingsProvider>();
        var assetProvider = _container.Resolve<IAssetProvider>();
        var localizationProvider = _container.Resolve<ILocalizationProvider>();
        var firstSceneProvider = _container.Resolve<FirstSceneProvider>();

        bootstrapLoader.AddForBootstrapInitialization(settingsProvider);
        bootstrapLoader.AddForBootstrapInitialization(assetProvider);
        bootstrapLoader.AddForBootstrapInitialization(localizationProvider);
        bootstrapLoader.AddForBootstrapInitialization(firstSceneProvider);

        Debug.Log("<color=green><b>Starting Services initialization...</b></color>");
        await bootstrapLoader.StartServicesInitializationAsync(100);
        Debug.Log("<color=green><b>End Services initialization...</b></color>");

        bootstrapUIModel.Clear();

        await bootstrapUIModel.FadeOut(1f);

        var bootstrapScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(firstSceneProvider.FirstScene.Scene);
        await SceneManager.UnloadSceneAsync(bootstrapScene);

        Debug.LogWarning("<color=green><b>=== APP STARTED! ===</b></color>");
    }
}
