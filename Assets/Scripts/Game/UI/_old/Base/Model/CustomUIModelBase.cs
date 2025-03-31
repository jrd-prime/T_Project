using Core.FSM.Data;
using UnityEngine;

namespace Game.UI._old.Base.Model
{
    public abstract class CustomUIModelBase : UIModelBase, IUIModel
    {
        public abstract void Initialize();

        public void GameStateChangeRequest(StateDataVo stateDataVo)
        {
            Debug.LogWarning("game state change request: " + stateDataVo.StateType + "." + stateDataVo.SubState);
            _ra.SetStateData(stateDataVo);
        }

        // public void SetPreviousState()
        // {
        //     var stateData = UIManager.GetPreviousState();
        //
        //     Debug.LogWarning(
        //         $"<color=darkblue>[Set PREVIOUS State]</color> Previous: {stateData.StateType}.{stateData.SubState}");
        //
        //     _ra.SetStateData(stateData);
        // }
    }
}
