using System.Collections.Generic;
using Game.UI.Interfaces;
using ModestTree;
using UnityEngine;

namespace Core.Managers.UI.Impls
{
    public class UIManager : MonoBehaviour, Interfaces.IUIManager
    {
        [SerializeField] private MainViewDataVo[] _mainViewDataVo;
        private readonly Dictionary<string, IUIView> _views = new();
        private readonly Stack<string> _viewStack = new();
        private string _baseViewId;

        private void Awake()
        {
            foreach (var viewDataVo in _mainViewDataVo) RegisterView(viewDataVo.viewId, viewDataVo.view);
            Log.Info("UIManager registered main views: " + _views.Count);
        }

        public void RegisterView(string viewId, IUIView view) => _views[viewId] = view;

        public void ShowView(string viewId)
        {
            Log.Info($"show view {viewId}");
            if (!_views.TryGetValue(viewId, out var view))
            {
                Log.Error($"view {viewId} not found");
                Debug.Break();
                return;
            }

            view.Show();
            if (_viewStack.Count == 0 || _viewStack.Peek() != viewId) _viewStack.Push(viewId);
        }

        public void HideView(string viewId)
        {
            Log.Info($"hide view {viewId}");
            if (_views.TryGetValue(viewId, out IUIView view)) view.Hide();
        }

        public void HideAllViews()
        {
            Log.Info("hide all views");
            foreach (var viewId in _viewStack) HideView(viewId);
            _viewStack.Clear();
            _baseViewId = null;
        }

        public void SwitchToView(string viewId)
        {
            Log.Info($"switch to view {viewId}");
            if (_viewStack.Count > 0 && _viewStack.Peek() != viewId) HideView(_viewStack.Peek());
            ShowView(viewId);
        }

        public void ShowPreviousView()
        {
            Log.Info("show previous view");
            if (_viewStack.Count <= 1) return;
            HideView(_viewStack.Pop());
            ShowView(_viewStack.Peek());
        }

        public void SetBaseView(string viewId)
        {
            Log.Info($"set base view {viewId}");
            // HideAllViews();
            _baseViewId = viewId;
            ShowView(viewId);
        }
    }
}
