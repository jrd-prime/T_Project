using System;
using Code.Core.WORK.JStateMachine.Data;
using R3;
using UnityEngine;

namespace Code.Core.WORK.JStateMachine
{
    public class StateMachineReactiveAdapter : IStateMachineReactiveAdapter
    {
        public ReactiveProperty<StateData> StateData { get; } = new();

        public void SetStateData(StateData stateData)
        {
            Debug.Log($"<b>State change requested to {stateData.State}.{stateData.SubState}</b>");
            StateData.Value = stateData;
        }

        public void Dispose()
        {
            StateData?.Dispose();
        }
    }

    public interface IStateMachineReactiveAdapter : IDisposable
    {
        public ReactiveProperty<StateData> StateData { get; }
        public void SetStateData(StateData stateData);
    }
}
