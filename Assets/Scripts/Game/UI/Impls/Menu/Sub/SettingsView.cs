using System;
using Game.UI.Interfaces;

namespace Game.UI.Impls.Menu.Sub
{
    public class SettingsView : IUIView
    {
        public void Show() => Console.WriteLine("Showing SettingsView");
        public void Hide() => Console.WriteLine("Hiding SettingsView");
    }
}
