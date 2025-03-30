using System.Collections.Generic;
using Core.FSM.Data;
using Game.UI.Interfaces;
using UnityEngine;

namespace Core.HSM
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        private readonly Dictionary<string, IUIView> _views = new();
        private readonly Stack<string> _viewStack = new();
        private string _baseViewId;

        public void RegisterView(string viewId, IUIView view) => _views[viewId] = view;

        public void ShowView(string viewId)
        {
            if (!_views.TryGetValue(viewId, out var view)) return;

            view.Show();
            if (_viewStack.Count == 0 || _viewStack.Peek() != viewId) _viewStack.Push(viewId);
        }

        public void HideView(string viewId)
        {
            if (_views.TryGetValue(viewId, out IUIView view)) view.Hide();
        }

        public void HideAllViews()
        {
            foreach (var viewId in _viewStack) HideView(viewId);
            _viewStack.Clear();
            _baseViewId = null;
        }

        public void SwitchToView(string viewId)
        {
            if (_viewStack.Count > 0 && _viewStack.Peek() != viewId) HideView(_viewStack.Peek());
            ShowView(viewId);
        }

        public void ShowPreviousView()
        {
            if (_viewStack.Count <= 1) return;
            HideView(_viewStack.Pop());
            ShowView(_viewStack.Peek());
        }

        public void SetBaseView(string viewId)
        {
            HideAllViews();
            _baseViewId = viewId;
            ShowView(viewId);
        }
    }
}
