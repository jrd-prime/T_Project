using Code.Core.WORK.Enums.States;
using Code.Core.WORK.UI.Base.Model;
using JetBrains.Annotations;
using R3;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.Core.WORK.JStateMachine.State.Gameplay.UI
{
    public interface IGameplayModel : IUIModel<EGameplaySubState>
    {
        // public ReadOnlyReactiveProperty<EnergySavableData> EnergyData { get; }
        // public ReadOnlyReactiveProperty<AmbientTempSavableData> AmbientTempData { get; }
        public ReadOnlyReactiveProperty<bool> IsGameRunning { get; }

        // public ReadOnlyReactiveProperty<DayTimerSavableData> CountdownData { get; }
        // public Subject<Unit> ShakeBackpackButton { get; }
        //
        // public void OnDownEvent(PointerDownEvent evt);
        // public void OnMoveEvent(PointerMoveEvent evt);
        // public void OnUpEvent(PointerUpEvent _);
        // public void OnOutEvent(PointerOutEvent _);
        // public void AddEnergy();
        // public void OpenBackpack();
        // public void ShakeBackpack();
    }

    [UsedImplicitly]
    public class GameplayModel : CustomUIModelBase<EGameplaySubState>, IGameplayModel
    {
        public Subject<Unit> ShakeBackpackButton { get; } = new();

        // public ReadOnlyReactiveProperty<DayTimerSavableData> CountdownData => _dayTimerDataModel.ModelData;
        public ReadOnlyReactiveProperty<bool> IsGameRunning => GameManager.IsGameRunning;
        // public ReadOnlyReactiveProperty<AmbientTempSavableData> AmbientTempData => _ambientTempDataModel.ModelData;
        // public ReadOnlyReactiveProperty<EnergySavableData> EnergyData => _energyDataModel.ModelData;

        // private IMovementControlModel _movementModel;
        // private EnergyDataModel _energyDataModel;
        // private AmbientTempDataModel _ambientTempDataModel;
        // private DayTimerDataModel _dayTimerDataModel;

        public override void Initialize()
        {
            // _movementModel = ResolverHelp.ResolveAndCheck<IMovementControlModel>(Resolver);
            // _energyDataModel = ResolverHelp.ResolveAndCheck<EnergyDataModel>(Resolver);
            // _ambientTempDataModel = ResolverHelp.ResolveAndCheck<AmbientTempDataModel>(Resolver);
            // _dayTimerDataModel = ResolverHelp.ResolveAndCheck<DayTimerDataModel>(Resolver);
        }

        // public void AddEnergy() => _energyDataModel.IncreaseEnergy(30);

        public void OpenBackpack()
        {
            Debug.LogWarning("OPEN BACKPACK11");
        }

        public void ShakeBackpack() => ShakeBackpackButton.OnNext(Unit.Default);

        // public void OnDownEvent(PointerDownEvent evt) => _movementModel.OnDownEvent(evt);
        // public void OnMoveEvent(PointerMoveEvent evt) => _movementModel.OnMoveEvent(evt);
        // public void OnUpEvent(PointerUpEvent _) => _movementModel.OnUpEvent(_);
        // public void OnOutEvent(PointerOutEvent _) => _movementModel.OnOutEvent(_);
    }
}
