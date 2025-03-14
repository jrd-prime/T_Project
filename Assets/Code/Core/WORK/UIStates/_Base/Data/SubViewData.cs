using System;
using Code.Core.WORK.UIStates._Base.View;

namespace Code.Core.WORK.UIStates._Base.Data
{
    [Serializable]
    public struct SubViewData<TSubStateEnum> where TSubStateEnum : Enum
    {
        public TSubStateEnum subState;
        public JSubViewBase subView;
    }
}
