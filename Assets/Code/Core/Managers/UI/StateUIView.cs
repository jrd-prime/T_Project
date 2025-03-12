using UnityEngine;
using UnityEngine.UIElements;

namespace Code.Core.Managers.UI
{
    public interface IStateUI
    {
        public void Show();
        public void Hide();
    }

    [RequireComponent(typeof(UIDocument))]
    public abstract class StateUIView : MonoBehaviour, IStateUI
    {
        public abstract void Show();

        public abstract void Hide();
    }
}
