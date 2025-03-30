//TODO turn on input after app start

using Bootstrap;
using Core.FSM.Data;
using Core.FSM.Interfaces;
using Core.HSM;
using Core.Providers;
using Core.Providers.Localization;
using Cysharp.Threading.Tasks;
using Game.UI._old.Menu.State;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public sealed class AppStarter : IInitializable
{
    private DiContainer _container;

    [Inject]
    private void Construct(DiContainer container) => _container = container;

    public void Initialize() => InitializeAsync().Forget();

    private async UniTask InitializeAsync()
    {
        Debug.LogWarning("app starter initialized");
        var bootstrapLoader = _container.Resolve<BootstrapLoader>();
        var bootstrapUIModel = _container.Resolve<IBootstrapUIModel>();

        var a = _container.Resolve<HSM>();

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

        var defStateData = new StateDataVo { StateType = GameStateType.Menu, SubState = MenuStateType.Main };
        var stateMachine = _container.Resolve<IGameStateDispatcher>();
        stateMachine.SetStateData(defStateData);

        await bootstrapUIModel.FadeOut(1f);

        var bootstrapScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(firstSceneProvider.FirstScene.Scene);
        await SceneManager.UnloadSceneAsync(bootstrapScene);

        Debug.LogWarning("<color=green><b>=== APP STARTED! ===</b></color>");
    }
}
