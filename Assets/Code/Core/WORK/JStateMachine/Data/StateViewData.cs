using System;
using Code.Core.WORK.Enums.States;
using Code.Core.WORK.UI.Base.View;

namespace Code.Core.WORK.JStateMachine.Data
{
    [Serializable]
    public struct StateViewData
    {
        public EGameState uiForState;
        public UIViewBase viewHolder;
    }
}
