using System;
using Code.Core.UI._Base.Data;
using UnityEngine;

namespace Code.Core.UI._Base.Model
{
    public abstract class CustomUIModelBase<TSubViewType> : UIModelBase, IUIModel<TSubViewType>
        where TSubViewType : Enum
    {
        public abstract void Initialize();

        public void GameStateChangeRequest(StateData stateData)
        {
            Debug.LogWarning("game state change request: " + stateData.StateType + "." + stateData.SubState);
            _ra.SetStateData(stateData);
        }

        public void SetPreviousState()
        {
            var stateData = UIManager.GetPreviousState();

            Debug.LogWarning(
                $"<color=darkblue>[Set PREVIOUS State]</color> Previous: {stateData.StateType}.{stateData.SubState}");

            _ra.SetStateData(stateData);
        }
    }
}
