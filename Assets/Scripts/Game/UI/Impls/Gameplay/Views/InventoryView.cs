﻿using Core.Extensions;
using Core.Providers.Localization;
using Db.Data;
using Game.UI.Common;
using Game.UI.Impls.Gameplay.Gameplay;
using Game.UI.Impls.Menu;
using UnityEngine.UIElements;

namespace Game.UI.Impls.Gameplay.Views
{
    public sealed class InventoryView : CustomViewBase<IGameplayViewModel>
    {
        private Button _playBtn;
        private Button _settingsBtn;
        private Button _exitBtn;

        protected override void InitializeView()
        {
            // _playBtn = ContentContainer.GetVisualElement<Button>(UIElementId.StartBtnId, name);
            // _settingsBtn = ContentContainer.GetVisualElement<Button>(UIElementId.SettingsBtnId, name);
            // _exitBtn = ContentContainer.GetVisualElement<Button>(UIElementId.ExitBtnId, name);
        }

        protected override void CreateAndInitComponents()
        {
        }

        protected override void Localize()
        {
            ViewMainHeader.text = LocalizationManager.Localize(LocalizationNameID.Inventory).ToUpper();
            // _playBtn.text = LocalizationManager.Localize(LocalizationNameID.Start).ToUpper();
            // _settingsBtn.text = LocalizationManager.Localize(LocalizationNameID.Settings).ToUpper();
            // _exitBtn.text = LocalizationManager.Localize(LocalizationNameID.Exit).ToUpper();
        }

        protected override void InitializeCallbacks()
        {
        }
    }
}
