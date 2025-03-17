using System;
using Code.UI._Base.View;

namespace Code.UI._Base.Data
{
    [Serializable]
    public struct SubViewData<TSubViewType> where TSubViewType : Enum
    {
        public TSubViewType subViewType;
        public SubViewBase subView;
    }
}
