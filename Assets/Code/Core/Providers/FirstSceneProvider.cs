using Bootstrap;
using Code.Core.SO;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Core.Providers
{
    public sealed class FirstSceneProvider : IBootable
    {
        public string Description => "Scene Provider";
        public SceneInstance FirstScene { get; private set; }

        private MainSettings _mainSettings;

        [Inject]
        private void Construct(MainSettings mainSettings) => _mainSettings = mainSettings;

        public async UniTask InitializationOnBoot()
        {
            FirstScene = await Addressables.LoadSceneAsync(_mainSettings.FirstScene, LoadSceneMode.Additive);
            // Debug.Log("Loaded scene: " + FirstScene.Scene.name + " (" + FirstScene.Scene.buildIndex + ")");
        }
    }
}
