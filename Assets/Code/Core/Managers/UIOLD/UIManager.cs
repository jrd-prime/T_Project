using System.Collections.Generic;
using Code.Core.Input;
using Code.Core.JStateMachineOLD;
using Code.Core.Providers;
using Code.Core.SO;
using Code.Core.UIOLD._Base.View;
using Code.Extensions;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Core.Managers.UIOLD
{
    public interface IUIManager
    {
    }

    public sealed class UIManager : MonoBehaviour, IUIManager, IInitializable
    {
        private IGameStateMachine _stateMachine;
        private ISettingsProvider _settingsProvider;
        private UIViewBase _current;
        private IObjectResolver _resolver;

        private readonly Dictionary<StateType, UIViewBase> _states = new();
        private readonly CompositeDisposable _disposables = new();

        private readonly Stack<UIViewBase> _viewStack = new Stack<UIViewBase>();
        private IJInput _input;

        [Inject]
        private void Construct(IObjectResolver resolver) => _resolver = resolver;

        public void Initialize()
        {
            _input = _resolver.ResolveAndCheckOnNull<IJInput>();

            _settingsProvider = _resolver.ResolveAndCheckOnNull<ISettingsProvider>();
            var viewsSettings = _settingsProvider.GetSettings<UIViewsSettings>();
            InitializeViews(viewsSettings);

            _stateMachine = _resolver.ResolveAndCheckOnNull<IGameStateMachine>();
            _stateMachine.GameState.DistinctUntilChanged().Subscribe(HandleGameState).AddTo(_disposables);

            if (_states.TryGetValue(StateType.Menu, out var initialView))
                ShowView(initialView);
        }

        private void InitializeViews(UIViewsSettings viewsSettings)
        {
            foreach (var viewSettings in viewsSettings.States)
            {
                var viewPrefab = viewSettings.sateUIViewBase;
                var viewInstance = _resolver.Instantiate(viewPrefab);

                viewInstance.transform.SetParent(transform, false);
                viewInstance.Hide();

                viewInstance.OnShowRequested
                    .Subscribe(view => ShowView(view))
                    .AddTo(_disposables);
                viewInstance.OnHideRequested
                    .Subscribe(_ => HideCurrentView())
                    .AddTo(_disposables);

                _states.TryAdd(viewSettings.GameState, viewInstance);
            }
        }

        public void ShowView(UIViewBase view)
        {
            if (_viewStack.Count > 0) _viewStack.Peek().Hide(); // Скрываем текущий верхний View

            _viewStack.Push(view);
            view.Show();
            Debug.LogWarning("View shown: " + view.name);
            SubscribeToInput(view);
        }

        private void SubscribeToInput(UIViewBase view)
        {
            _input.OnEscape
                .Subscribe(_ => view.RequestHide())
                .AddTo(_disposables);
            // Добавь другие подписки на ввод, если нужно
        }

        public void HideCurrentView()
        {
            if (_viewStack.Count > 0)
            {
                var currentView = _viewStack.Pop();
                currentView.Hide();
                Debug.LogWarning("View hidden: " + currentView.name);
            }

            if (_viewStack.Count > 0) _viewStack.Peek().Show(); // Показываем предыдущий View
        }

        private void HandleGameState(StateType state)
        {
            if (!_states.TryGetValue(state, out var uiState))
                throw new KeyNotFoundException($"{state} not found!");

            ShowView(uiState);
        }
    }
}
