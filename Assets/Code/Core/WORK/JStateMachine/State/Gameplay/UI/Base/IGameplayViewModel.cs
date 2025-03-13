using Code.Core.WORK.UI.Base.ViewModel;
using R3;
using UnityEngine.UIElements;

namespace Code.Core.WORK.JStateMachine.State.Gameplay.UI.Base
{
    public interface IGameplayViewModel : IUIViewModel
    {
        // public Subject<Unit> MenuBtnClicked { get; }
        // public Subject<Unit> CloseBtnClicked { get; }
        // public Subject<Unit> AddEnergyBtnClicked { get; }
        // public Subject<Unit> BackpackBtnClicked { get; }
        //
        //
        // public Subject<Unit> ShakeBackpackButton { get; }

        // public ReadOnlyReactiveProperty<EnergySavableData> ShelterEnergyData { get; }
        // public ReadOnlyReactiveProperty<AmbientTempSavableData> AmbientTemperatureData { get; }
        public ReadOnlyReactiveProperty<bool> IsGameRunning { get; }

        // public ReactiveProperty<PreparedDayTimerData> PreparedDayTimerData { get; }
        // public ReactiveProperty<PreparedEnergyData> PreparedEnergyData { get; }
        // public ReactiveProperty<PreparedTemperatureData> PreparedTemperatureData { get; }
        //
        // public void OnDownEvent(PointerDownEvent evt);
        // public void OnMoveEvent(PointerMoveEvent evt);
        // public void OnUpEvent(PointerUpEvent evt);
        // public void OnOutEvent(PointerOutEvent evt);
    }
}
