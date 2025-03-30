using System;
using Game.UI.Interfaces;

namespace Game.UI.Impls.Gameplay
{
    public class GameplayMainView : IUIView
    {
        public void Show() => Console.WriteLine("Showing GameplayMainView");
        public void Hide() => Console.WriteLine("Hiding GameplayMainView");
    }
}
