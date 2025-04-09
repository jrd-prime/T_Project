using Cysharp.Threading.Tasks;
using Data.SO;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Infrastructure.Bootstrap
{
    public sealed class FirstSceneProvider : IBootable
    {
        public string Description => "Scene Provider";
        public SceneInstance FirstScene { get; private set; }

        private readonly MainSettings _mainSettings;

        public FirstSceneProvider(MainSettings mainSettings) => _mainSettings = mainSettings;

        public async UniTask InitializationOnBoot()
        {
            FirstScene = await Addressables.LoadSceneAsync(_mainSettings.FirstScene, LoadSceneMode.Additive);
        }
    }
}
