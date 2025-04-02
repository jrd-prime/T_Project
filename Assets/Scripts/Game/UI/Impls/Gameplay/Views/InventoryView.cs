using System;
using Game.UI.Interfaces;

namespace Game.UI.Impls.Gameplay.Views
{
    public class InventoryView : IUIView
    {
        public void Show() => Console.WriteLine("Showing InventoryView");
        public void Hide() => Console.WriteLine("Hiding InventoryView");
    }
}
