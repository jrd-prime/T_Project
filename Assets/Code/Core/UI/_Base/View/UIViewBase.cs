using UnityEngine;
using UnityEngine.UIElements;

namespace Code.Core.UI._Base.View
{
    public interface IUIView
    {
        public void Show();
        public void Hide();
    }

    [RequireComponent(typeof(UIDocument))]
    public abstract class UIViewBase : MonoBehaviour, IUIView
    {
        public abstract void Show();

        public abstract void Hide();
    }
}
