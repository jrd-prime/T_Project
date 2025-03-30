using System;
using Game.UI.Interfaces;

namespace Game.UI.Impls.Gameplay
{
    public class GameplayHUD : IUIView
    {
        public void Show() => Console.WriteLine("Showing GameplayHUD");
        public void Hide() => Console.WriteLine("Hiding GameplayHUD");
    }
}
