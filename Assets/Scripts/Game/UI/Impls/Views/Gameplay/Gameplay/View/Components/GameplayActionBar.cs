﻿using Game.Extensions;
using Game.UI.Common;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UI.Impls.Views.Gameplay.Gameplay.View.Components
{
    public sealed class GameplayActionBar : ViewComponentBase<IGameplayViewModel>
    {
        private VisualElement _actionBarContainer;

        public GameplayActionBar(ViewComponentBaseData<IGameplayViewModel> data) : base(data)
        {
        }


        protected override void InitializeVisualElements()
        {
            _actionBarContainer =
                Root.GetVisualElement<VisualElement>("action-bar-container", nameof(GameplayActionBar));
            _actionBarContainer.style.backgroundColor = new StyleColor(new Color(0.6f, 0.7f, 0.5f, 0.8f));
        }

        protected override void Localize()
        {
        }

        protected override void InitializeSubscriptions()
        {
        }
    }
}
