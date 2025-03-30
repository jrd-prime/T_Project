using System;

namespace Core.FSM.Data
{
    public struct StateDataVo
    {
        public GameStateType StateType;
        public Enum SubState;

        public StateDataVo(GameStateType baseStateType, Enum oSubState = null)
        {
            StateType = baseStateType;
            SubState = oSubState;
        }
    }
}
