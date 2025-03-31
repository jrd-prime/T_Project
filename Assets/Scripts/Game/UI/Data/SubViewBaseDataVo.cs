using System;
using Game.UI.Common;
using UnityEngine.Serialization;

namespace Game.UI.Data
{
    [Serializable]
    public struct SubViewBaseDataVo
    {
        public string subViewId;
        [FormerlySerializedAs("subView")] public AViewBase view;
    }
}
