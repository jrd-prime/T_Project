using System;

namespace Core.FSM.Data
{
    public struct StateDataVo
    {
        public GameStateType StateType;
        public string SubState;

        public StateDataVo(GameStateType baseStateType, string oSubState = null)
        {
            StateType = baseStateType;
            SubState = oSubState;
        }
    }
}
