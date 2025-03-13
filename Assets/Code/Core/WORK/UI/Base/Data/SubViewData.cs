using System;
using Code.Core.WORK.UI.Base.View;

namespace Code.Core.WORK.UI.Base.Data
{
    [Serializable]
    public struct SubViewData<TSubStateEnum> where TSubStateEnum : Enum
    {
        public TSubStateEnum subState;
        public JSubViewBase subView;
    }
}
