using System;
using Code.Core.Data;
using Code.Hero;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Core.SO
{
    [CreateAssetMenu(
        fileName = nameof(MainSettings),
        menuName = SOPathConst.MainSettings + nameof(MainSettings),
        order = 0)]
    public class MainSettings : SettingsBase
    {
        [field: SerializeField] public AssetReferenceT<SceneAsset> FirstScene { get; private set; }
        [field: SerializeField] public HeroSettings HeroSettings { get; private set; }


        private void OnValidate()
        {
            if (FirstScene == null) throw new Exception("FirstScene is null or invalid. " + name);
            if (HeroSettings == null) throw new Exception("HeroSettings is null or invalid. " + name);
        }
    }
}
