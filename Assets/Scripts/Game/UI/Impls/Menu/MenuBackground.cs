using System;
using Game.UI.Interfaces;

namespace Game.UI.Impls.Menu
{
    public class MenuBackground : IUIView
    {
        public void Show() => Console.WriteLine("Showing MenuBackground");
        public void Hide() => Console.WriteLine("Hiding MenuBackground");
    }
}
