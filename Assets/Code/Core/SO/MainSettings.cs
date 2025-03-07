using System;
using Code.Core.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Core.SO
{
    [CreateAssetMenu(
        fileName = nameof(MainSettings),
        menuName = SOPathConst.Settings + nameof(MainSettings),
        order = 100)]
    public class MainSettings : SettingsBase
    {
        [field: SerializeField] public AssetReferenceT<SceneAsset> FirstScene { get; private set; }

        private void OnValidate()
        {
            if (FirstScene == null) throw new Exception("FirstScene is null or invalid. " + name);
        }
    }
}
