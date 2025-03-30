using System;
using Game.UI.Interfaces;

namespace Game.UI.Impls.Menu.Sub
{
    public class EffectsSettingsView : IUIView
    {
        public void Show() => Console.WriteLine("Showing EffectsSettingsView");
        public void Hide() => Console.WriteLine("Hiding EffectsSettingsView");
    }
}
