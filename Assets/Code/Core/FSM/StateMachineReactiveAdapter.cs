using System;
using Code.Core.UI;
using R3;
using UnityEngine;

namespace Code.Core.FSM
{
    public interface IStateMachineReactiveAdapter : IDisposable
    {
        public ReactiveProperty<StateData> StateData { get; }
        public void SetStateData(StateData stateData);
    }

    public class StateMachineReactiveAdapter : IStateMachineReactiveAdapter
    {
        public ReactiveProperty<StateData> StateData { get; } = new();

        public void SetStateData(StateData stateData)
        {
            Debug.Log($"<b>State change requested to {stateData.StateType}.{stateData.SubState}</b>");
            StateData.Value = stateData;
        }

        public void Dispose()
        {
            StateData?.Dispose();
        }
    }
}
