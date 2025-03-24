using Bootstrap;
using Code.Core.FSM;
using Code.Core.Providers;
using Code.Core.Providers.Localization;
using Code.Extensions;
using Code.UI._Base.Data;
using Code.UI.Menu.State;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

//TODO turn on input after app start
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
        var localizationProvider = _resolver.ResolveAndCheckOnNull<ILocalizationProvider>();
        var firstSceneProvider = _resolver.ResolveAndCheckOnNull<FirstSceneProvider>();

        bootstrapLoader.AddForBootstrapInitialization(settingsProvider);
        bootstrapLoader.AddForBootstrapInitialization(assetProvider);
        bootstrapLoader.AddForBootstrapInitialization(localizationProvider);
        bootstrapLoader.AddForBootstrapInitialization(firstSceneProvider);

        Debug.Log("<color=green><b>Starting Services initialization...</b></color>");
        await bootstrapLoader.StartServicesInitializationAsync(100);
        Debug.Log("<color=green><b>End Services initialization...</b></color>");

        bootstrapUIModel.Clear();

        var defStateData = new StateData { StateType = GameStateType.Menu, SubState = MenuStateType.Main };
        var stateMachine = _resolver.ResolveAndCheckOnNull<IStateMachineReactiveAdapter>();
        stateMachine.SetStateData(defStateData);

        await bootstrapUIModel.FadeOut(1f);

        var bootstrapScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(firstSceneProvider.FirstScene.Scene);
        await SceneManager.UnloadSceneAsync(bootstrapScene);

        Debug.LogWarning("<color=green><b>=== APP STARTED! ===</b></color>");
    }
}