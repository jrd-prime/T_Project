using System;
using Code.Core.WORK.JStateMachine;
using Code.Core.WORK.UI.Base.View;

namespace Code.Core.WORK.UIStates
{
    [Serializable]
    public struct StateViewData
    {
        public GameStateType uiForStateType;
        public UIViewBase viewHolder;
    }
}
