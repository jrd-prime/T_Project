using System;
using Core.FSM.Data;
using R3;

namespace Core.FSM.Interfaces
{
    public interface IGameStateDispatcher : IDisposable
    {
        public ReactiveProperty<StateDataVo> StateData { get; }
        public void SetStateData(StateDataVo stateDataVo);
    }
}
