using Code.Core.UI._Base.Model;
using Code.Core.UI.Gameplay.State;
using JetBrains.Annotations;
using R3;
using UnityEngine;

namespace Code.Core.UI.Gameplay
{
    public interface IGameplayModel : IUIModel<GameplayStateType>
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
    public class GameplayModel : CustomUIModelBase<GameplayStateType>, IGameplayModel
    {
        // private IStateMachine fsm;
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

            // fsm = Resolver.ResolveAndCheckOnNull<IStateMachine>();
            //
            // var input = Resolver.ResolveAndCheckOnNull<IJInput>();
            // input.OnEscape.Subscribe(HandleEscapeClick).AddTo(Disposables);
        }

        // private void HandleEscapeClick(Unit _)
        // {
        //     var state = fsm.CurrentState.CurrentValue;
        //     
        //     if (fsm.CurrentState.CurrentValue == GameStateType.Gameplay)
        //     {
        //         fsm.ChangeState(GameStateType.Menu);
        //     }
        // }
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
