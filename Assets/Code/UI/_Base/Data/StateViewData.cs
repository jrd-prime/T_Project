using System;
using Code.Core.FSM;
using Code.UI._Base.View;

namespace Code.UI._Base.Data
{
    [Serializable]
    public struct StateViewData
    {
        public GameStateType uiForStateType;
        public ViewBase viewHolder;
    }
}
