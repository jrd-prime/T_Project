using System;
using Db.Data;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Db.SO
{
    [CreateAssetMenu(
        fileName = nameof(MainSettings),
        menuName = SOPathConst.MainSettings + nameof(MainSettings),
        order = 0)]
    public class MainSettings : SettingsBase
    {
        [field: SerializeField] public AssetReference FirstScene { get; private set; }
        [field: SerializeField] public LocalizationSettings LocalizationSettings { get; private set; }
        [field: SerializeField] public HeroSettings HeroSettings { get; private set; }
        [field: SerializeField] public UIViewsSettings UIViewsSettings { get; private set; }


        private void OnValidate()
        {
            if (FirstScene == null) throw new Exception("FirstScene is null or invalid. " + name);
            if (!LocalizationSettings)
                throw new Exception($"{nameof(LocalizationSettings)} is null or invalid. " + name);
            if (!HeroSettings) throw new Exception($"{nameof(HeroSettings)} is null or invalid. " + name);
            if (!UIViewsSettings) throw new Exception($"{nameof(UIViewsSettings)} is null or invalid. " + name);
        }
    }
}
