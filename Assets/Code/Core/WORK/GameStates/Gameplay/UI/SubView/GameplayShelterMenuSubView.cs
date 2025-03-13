using System;
using Code.Core.WORK.GameStates.Gameplay.UI.Base;
using Code.Core.WORK.UI.Base.View;
using UnityEngine.UIElements;

namespace Code.Core.WORK.GameStates.Gameplay.UI.SubView
{
    public class GameplayShelterMenuSubView : CustomSubViewBase<IGameplayViewModel>
    {
        private Button _closeBtn;

        // private HealthBar _healthBarComponent;
        // private ExperienceBar _experienceBarComponent;

        protected override void InitializeView()
        {
            // _closeBtn = ContentContainer.Q<Button>(UIElementId.CloseBtnIDName).CheckOnNull();
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
