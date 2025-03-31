using System;
using Game.UI.Common;

namespace Game.UI._old.Base.Data
{
    [Serializable]
    public struct SubViewData<TSubViewType> where TSubViewType : Enum
    {
        public TSubViewType subViewType;
        public SubViewBase subView;
    }
}
