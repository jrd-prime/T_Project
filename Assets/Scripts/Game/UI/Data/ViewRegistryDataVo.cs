using System;
using Core.Managers.UI.Data;
using Game.UI.Common;

namespace Game.UI.Data
{
    [Serializable]
    public struct ViewRegistryDataVo
    {
        public ViewRegistryType type;
        public AViewRegistryBase viewRegistry;
    }
}
