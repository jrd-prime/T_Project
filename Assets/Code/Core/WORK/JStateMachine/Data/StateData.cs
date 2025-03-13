using System;
using Code.Core.WORK.Enums.States;

namespace Code.Core.WORK.JStateMachine.Data
{
    public struct StateData
    {
        public EGameState State;
        public Enum SubState;

        public StateData(EGameState baseState, Enum oSubState = default)
        {
            State = baseState;
            SubState = oSubState;
        }
    }
}
