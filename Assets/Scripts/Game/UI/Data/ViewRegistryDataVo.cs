using System;
using Game.UI.Common;

namespace Game.UI.Data
{
    [Serializable]
    public struct ViewRegistryDataVo
    {
        public ViewRegistryType type;
        public AViewRegistryBase viewRegistry;
    }

    public enum ViewRegistryType
    {
        NotSet = -1,
        Gameplay = 0,
        Menu = 1,
        Pause = 2
    }
}
