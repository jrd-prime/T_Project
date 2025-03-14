using System;
using Code.Core.FSM;
using Code.Core.UI._Base.View;

namespace Code.Core.UI
{
    [Serializable]
    public struct StateViewData
    {
        public GameStateType uiForStateType;
        public ViewBase viewHolder;
    }
}
