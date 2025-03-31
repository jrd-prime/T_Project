using Core.FSM.Data;
using Game.UI._old.Base.ViewModel;
using R3;

namespace Game.UI._old.Gameplay
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

    public class GameplayViewModel : UIViewModelBase<IGameplayModel>, IGameplayViewModel
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
            MenuBtnClicked.Subscribe(_ => Model.GameStateChangeRequest(new StateDataVo(GameStateType.Menu))).AddTo(Disposables);
            // CloseBtnClicked.Subscribe(_ => Model.SetPreviousState()).AddTo(Disposables);
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
