// using Code.Core.WORK.JStateMachine.State.Gameplay.UI.Base;
// using Code.Core.WORK.UI.Base.Component;
// using R3;
// using UnityEngine.UIElements;
//
// namespace Code.Core.WORK.JStateMachine.State.Gameplay.UI.Components
// {
//     public sealed class AmbientTempTimerView : ViewComponentBase<IGameplayViewModel>
//     {
//         private Label _currentTempLabel;
//         private Label _nextDropLabel;
//
//         public AmbientTempTimerView(IGameplayViewModel viewModel, in VisualElement root,
//             in CompositeDisposable disposables)
//             : base(viewModel, root, disposables)
//         {
//         }
//
//         protected override void InitializeVisualElements()
//         {
//             _currentTempLabel = Root.Q<Label>("cur-label").CheckOnNull();
//             _nextDropLabel = Root.Q<Label>("next-down-label").CheckOnNull();
//         }
//
//         protected override void InitializeSubscriptions()
//         {
//             ViewModel.PreparedTemperatureData
//                 .Subscribe(UpdateTemperatureData)
//                 .AddTo(Disposables);
//         }
//
//         private void UpdateTemperatureData(PreparedTemperatureData data)
//         {
//             _currentTempLabel.text = data.Current;
//             _nextDropLabel.text = data.NextChange;
//         }
//     }
// }



