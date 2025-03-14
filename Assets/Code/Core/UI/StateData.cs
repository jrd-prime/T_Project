using System;
using Code.Core.FSM;

namespace Code.Core.UI
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
