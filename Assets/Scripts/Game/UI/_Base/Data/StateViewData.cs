using System;
using Core.FSM.Data;
using Game.UI._Base.View;

namespace Game.UI._Base.Data
{
    [Serializable]
    public struct StateViewData
    {
        public GameStateType uiForStateType;
        public ViewBase viewHolder;
    }
}
