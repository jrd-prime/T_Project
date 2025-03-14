using System;
using Code.Core.UI._Base.View;

namespace Code.Core.UI._Base.Data
{
    [Serializable]
    public struct SubViewData<TSubViewType> where TSubViewType : Enum
    {
        public TSubViewType subViewType;
        public SubViewBase subView;
    }
}
