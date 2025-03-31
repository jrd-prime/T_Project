using System;
using Core.FSM.Data;
using Game.UI.Common;

namespace Game.UI._old.Base.Data
{
    [Serializable]
    public struct StateViewData
    {
        public GameStateType uiForStateType;
        public ViewBase viewHolder;
    }
}
