using System;
using Game.UI.Common;
using UnityEngine.Serialization;

namespace Game.UI.Data
{
    [Serializable]
    public struct ViewRegistryDataVo
    {
        public ViewRegistryType type;
        [FormerlySerializedAs("viewRegistry")] public AViewRegistryBase aViewRegistry;
    }

    public enum ViewRegistryType
    {
        NotSet = -1,
        Gameplay = 0,
        Menu = 1
    }
}
