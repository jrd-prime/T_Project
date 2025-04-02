using System;
using UnityEngine.Serialization;

namespace Game.UI.Common.Base.Data
{
    [Serializable]
    public struct SubViewData<TSubViewType> where TSubViewType : Enum
    {
        public TSubViewType subViewType;
        [FormerlySerializedAs("aSubView")] [FormerlySerializedAs("view")] public AViewBase aView;
    }
}
