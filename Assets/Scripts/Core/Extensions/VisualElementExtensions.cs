using System;
using UnityEngine.UIElements;

namespace Core.Extensions
{
    public static class VisualElementExtensions
    {
        public static T GetVisualElement<T>(this VisualElement root, string elementIDName, string goName)
            where T : VisualElement
        {
            var element = root.Q<T>(elementIDName);
            if (element == null) throw new NullReferenceException($"{elementIDName} in {goName} not found");
            return element;
        }

        public static VisualElement SetFullScreen(this VisualElement element)
        {
            var style = element.style;
            style.position = Position.Absolute;
            style.left = style.top = style.right = style.bottom = 0f;
            return element;
        }

        public static VisualElement ContentToCenter(this VisualElement element)
        {
            var style = element.style;
            style.alignContent = Align.Center;
            style.alignItems = Align.Center;
            style.alignSelf = Align.Center;
            style.justifyContent = Justify.Center;
            return element;
        }
    }
}
