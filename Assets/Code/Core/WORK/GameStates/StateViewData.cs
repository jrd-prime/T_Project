using System;
using Code.Core.WORK.Enums.States;
using Code.Core.WORK.UI.Base.View;

namespace Code.Core.WORK.GameStates
{
    [Serializable]
    public struct StateViewData
    {
        public EGameState uiForState;
        public UIViewBase viewHolder;
    }
}
