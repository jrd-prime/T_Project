using System;
using Code.Core.UI._Base.Component;
using Code.Core.UI._Base.View;
using Code.Core.UI.Gameplay.View.Components;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.Core.UI.Gameplay.View
{
    public class GameplayViewMain : CustomSubViewBase<IGameplayViewModel>
    {
        private Button _menuBtn;
        private Button _addEnergyBtn;
        private Button _backpackBtn;

        // private HealthBar _healthBarComponent;
        // private ExperienceBar _experienceBarComponent;
        // private EnergyTimerView _energyTimerView;
        // private AmbientTempTimerView _ambientTempTimerView;
        // private DayCountdownTimerView _dayCountdownTimerView;

        private const float ShakeMaxScale = 1.2f;
        private const float ShakeMinScale = 0.9f;
        private const float ShakeDuration = 0.1f;

        protected override void InitializeView()
        {
            
            
            // _menuBtn = ContentContainer.Q<Button>("menu-btn").CheckOnNull();
            // _addEnergyBtn = ContentContainer.Q<Button>("add-energy-btn").CheckOnNull();
            // _backpackBtn = ContentContainer.Q<Button>("backpack-btn").CheckOnNull();
        }

        protected override void CreateAndInitComponents()
        {
            if (ViewModel == null) throw new NullReferenceException("ViewModel is null");

            var viewComponentBaseData = new ViewComponentBaseData<IGameplayViewModel>(ViewModel, ContentContainer);
            
            // ViewModel.ShakeBackpackButton.Subscribe(_ => ShakeBackpackButton()).AddTo(Disposables);

            var _actionBar = new GameplayActionBar(viewComponentBaseData);

            // _energyTimerView = new EnergyTimerView(ViewModel, ContentContainer, Disposables);
            // _ambientTempTimerView = new AmbientTempTimerView(ViewModel, ContentContainer, Disposables);
            // _dayCountdownTimerView = new DayCountdownTimerView(ViewModel, ContentContainer, Disposables);
        }

        protected override void Localize()
        {
        }

        protected override void InitializeCallbacks()
        {
            // CallbacksCache.TryAdd(_menuBtn, _ => ViewModel.MenuBtnClicked.OnNext(Unit.Default));
            // CallbacksCache.TryAdd(_addEnergyBtn, _ => ViewModel.AddEnergyBtnClicked.OnNext(Unit.Default));
            // CallbacksCache.TryAdd(_backpackBtn, _ => ViewModel.BackpackBtnClicked.OnNext(Unit.Default));
        }

        private void ShakeBackpackButton()
        {
            var sequence = DOTween.Sequence();

            sequence.Append(DOTween.To(
                () => _backpackBtn.resolvedStyle.scale.value,
                x => _backpackBtn.style.scale = x,
                new Vector3(ShakeMaxScale, ShakeMaxScale, 1f),
                ShakeDuration
            ));

            sequence.Append(DOTween.To(
                () => _backpackBtn.resolvedStyle.scale.value,
                x => _backpackBtn.style.scale = x,
                new Vector3(ShakeMinScale, ShakeMinScale, 1f),
                ShakeDuration
            ));
            sequence.OnComplete(() =>
            {
                DOTween.To(
                    () => _backpackBtn.resolvedStyle.scale.value,
                    x => _backpackBtn.style.scale = x,
                    Vector3.one,
                    ShakeDuration
                );
            });
        }
    }
}
