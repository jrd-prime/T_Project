﻿using Cysharp.Threading.Tasks;
using Infrastructure.Bootstrap;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Infrastructure.Assets
{
    public interface IAssetProvider : IBootable
    {
        UniTask<SceneInstance> LoadSceneAsync(string assetId, LoadSceneMode loadSceneMode);
        UniTask<GameObject> InstantiateAsync(AssetReference assetId, Transform parent = null);
        GameObject Instantiate(AssetReferenceGameObject assetId, Transform parent = null);
    }

    [UsedImplicitly]
    public sealed class AssetProvider : IAssetProvider
    {
        public string Description => "Asset Provider";
        public async UniTask InitializationOnBoot() => await Addressables.InitializeAsync();

        public async UniTask<SceneInstance> LoadSceneAsync(string assetId, LoadSceneMode loadSceneMode) =>
            await Addressables.LoadSceneAsync(AssetNameConst.GameScene, loadSceneMode);

        public async UniTask<GameObject> InstantiateAsync(AssetReference assetId, Transform parent = null)
        {
            var handle = Addressables.InstantiateAsync(assetId, parent);
            return await handle.Task;
        }

        public GameObject Instantiate(AssetReferenceGameObject assetId, Transform parent = null) =>
            Addressables.InstantiateAsync(assetId, parent).Result;
    }
}
