using System;
using Game.UI.Interfaces;

namespace Game.UI.Impls.Menu
{
    public class MenuView : IUIView
    {
        public void Show() => Console.WriteLine("Showing MenuView");
        public void Hide() => Console.WriteLine("Hiding MenuView");
    }
}
