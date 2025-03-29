using Core.FSM.Data;
using Core.FSM.Interfaces;
using R3;
using UnityEngine;

namespace Core.FSM
{
    //TODO strange name for this class
    public class GameGameStateDispatcher : IGameStateDispatcher
    {
        public ReactiveProperty<StateDataVo> StateData { get; } = new();

        public void SetStateData(StateDataVo stateDataVo)
        {
            Debug.LogWarning("New state request: " + stateDataVo.StateType + " / " + stateDataVo.SubState);
            StateData.Value = stateDataVo;
        }

        public void Dispose() => StateData?.Dispose();
    }
}
