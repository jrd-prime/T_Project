using System;
using Code.Core.WORK.JStateMachine;

namespace Code.Core.WORK.UIStates
{
    public struct StateData
    {
        public GameStateType StateType;
        public Enum SubState;

        public StateData(GameStateType baseStateType, Enum oSubState = default)
        {
            StateType = baseStateType;
            SubState = oSubState;
        }
    }
}
