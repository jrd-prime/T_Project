using System;
using Code.UI._Base.Data;
using JetBrains.Annotations;
using R3;
using UnityEngine;

namespace Code.Core.FSM
{
    public interface IStateMachineReactiveAdapter : IDisposable
    {
        public ReactiveProperty<StateData> StateData { get; }
        public void SetStateData(StateData stateData);
    }

    //TODO strange name for this class
    [UsedImplicitly]
    public class StateMachineReactiveAdapter : IStateMachineReactiveAdapter
    {
        public ReactiveProperty<StateData> StateData { get; } = new();

        public void SetStateData(StateData stateData)
        {
            Debug.LogWarning("New state request: " + stateData.StateType + " / " + stateData.SubState);
            StateData.Value = stateData;
        }

        public void Dispose() => StateData?.Dispose();
    }
}
