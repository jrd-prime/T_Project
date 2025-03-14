using System;
using Code.Core.WORK.JStateMachine;
using Code.Core.WORK.UIStates._Base.View;

namespace Code.Core.WORK.UIStates
{
    [Serializable]
    public struct StateViewData
    {
        public GameStateType uiForStateType;
        public UIViewBase viewHolder;
    }
}
