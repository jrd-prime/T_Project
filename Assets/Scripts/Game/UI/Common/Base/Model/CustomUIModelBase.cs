namespace Game.UI.Common.Base.Model
{
    public abstract class CustomUIModelBase : UIModelBase, IUIModel
    {
        public abstract void Initialize();



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
