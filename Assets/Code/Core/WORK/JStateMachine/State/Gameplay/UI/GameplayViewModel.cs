using Code.Core.WORK.Enums.States;
using Code.Core.WORK.JStateMachine.Data;
using Code.Core.WORK.JStateMachine.State.Gameplay.UI.Base;
using Code.Core.WORK.UI.Base.ViewModel;
using R3;
using UnityEngine.UIElements;

namespace Code.Core.WORK.JStateMachine.State.Gameplay.UI
{
    public class GameplayViewModel : UIViewModelBase<IGameplayModel, EGameplaySubState>, IGameplayViewModel
    {
        #region Click

        public Subject<Unit> MenuBtnClicked { get; } = new();
        public Subject<Unit> CloseBtnClicked { get; } = new();
        public Subject<Unit> AddEnergyBtnClicked { get; } = new();
        public Subject<Unit> BackpackBtnClicked { get; } = new();

        #endregion

        // public ReadOnlyReactiveProperty<EnergySavableData> ShelterEnergyData => Model.EnergyData;
        // public ReadOnlyReactiveProperty<AmbientTempSavableData> AmbientTemperatureData => Model.AmbientTempData;
        public ReadOnlyReactiveProperty<bool> IsGameRunning => Model.IsGameRunning;
        // public ReactiveProperty<PreparedDayTimerData> PreparedDayTimerData { get; } = new();
        // public ReactiveProperty<PreparedEnergyData> PreparedEnergyData { get; } = new();
        // public ReactiveProperty<PreparedTemperatureData> PreparedTemperatureData { get; } = new();
        // public Subject<Unit> ShakeBackpackButton => Model.ShakeBackpackButton;

        // private DayCountdownUpdater _dayCountdownUpdater;
        // private EnergyUpdater _energyUpdater;
        // private TemperatureUpdater _temperatureUpdater;

        public override void Initialize()
        {
            // _dayCountdownUpdater = new DayCountdownUpdater();
            // _energyUpdater = new EnergyUpdater();
            // _temperatureUpdater = new TemperatureUpdater();

            // Model.CountdownData.Subscribe(UpdateDayTimerData).AddTo(Disposables);
            // Model.EnergyData.Subscribe(UpdateEnergyData).AddTo(Disposables);
            // Model.AmbientTempData.Subscribe(UpdateTemperatureData).AddTo(Disposables);

            // Buttons
            MenuBtnClicked.Subscribe(_ => Model.SetGameState(new StateData(EGameState.Menu))).AddTo(Disposables);
            CloseBtnClicked.Subscribe(_ => Model.SetPreviousState()).AddTo(Disposables);
            // AddEnergyBtnClicked.Subscribe(_ => Model.AddEnergy()).AddTo(Disposables);
            // BackpackBtnClicked.Subscribe(_ => Model.OpenBackpack()).AddTo(Disposables);
        }

        // private void UpdateDayTimerData(DayTimerSavableData savableData)
        // {
        //     PreparedDayTimerData.Value = _dayCountdownUpdater.Update(savableData);
        //     PreparedDayTimerData.NotifyIfDataIsClass();
        // }
        //
        // private void UpdateEnergyData(EnergySavableData savableData)
        // {
        //     PreparedEnergyData.Value = _energyUpdater.Update(savableData);
        //     PreparedEnergyData.NotifyIfDataIsClass();
        // }
        //
        // private void UpdateTemperatureData(AmbientTempSavableData savableData)
        // {
        //     PreparedTemperatureData.Value = _temperatureUpdater.Update(savableData);
        //     PreparedTemperatureData.NotifyIfDataIsClass();
        // }

        #region Move

        // public void OnDownEvent(PointerDownEvent evt) => Model.OnDownEvent(evt);
        // public void OnMoveEvent(PointerMoveEvent evt) => Model.OnMoveEvent(evt);
        // public void OnUpEvent(PointerUpEvent evt) => Model.OnUpEvent(evt);
        // public void OnOutEvent(PointerOutEvent evt) => Model.OnOutEvent(evt);

        #endregion
    }
}
