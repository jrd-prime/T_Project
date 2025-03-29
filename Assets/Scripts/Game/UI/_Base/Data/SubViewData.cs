using System;
using Game.UI._Base.View;

namespace Game.UI._Base.Data
{
    [Serializable]
    public struct SubViewData<TSubViewType> where TSubViewType : Enum
    {
        public TSubViewType subViewType;
        public SubViewBase subView;
    }
}
