using System;
using Game.UI.Interfaces;

namespace Game.UI.Impls.Menu.Sub
{
    public class AudioSettingsView : IUIView
    {
        public void Show() => Console.WriteLine("Showing AudioSettingsView");
        public void Hide() => Console.WriteLine("Hiding AudioSettingsView");
    }
}
