using System;
using Game.UI.Common;
using UnityEngine.UIElements;

namespace Game.UI.Impls.Views.Gameplay.Gameplay.View
{
    public class GameplayViewShelterMenu : CustomViewBase<IGameplayViewModel>
    {
        private Button _closeBtn;

        // private HealthBar _healthBarComponent;
        // private ExperienceBar _experienceBarComponent;

        protected override void InitializeView()
        {
            // _closeBtn = RootContainer.Q<Button>(UIElementId.CloseBtnIDName).CheckOnNull();
        }

        protected override void CreateAndInitComponents()
        {
            if (ViewModel == null)
                throw new NullReferenceException("ViewModel is null");
        }

        protected override void Localize()
        {
        }

        protected override void InitializeCallbacks()
        {
            // CallbacksCache.TryAdd(_closeBtn, _ => ViewModel.CloseBtnClicked.OnNext(Unit.Default));
        }
    }
}
