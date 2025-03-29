using System;
using Core.Extensions;
using Db.Data;
using R3;
using Tools;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Bootstrap
{
    [RequireComponent(typeof(UIDocument))]
    public sealed class BootstrapUIView : MonoBehaviour
    {
        private const string BootstrapContainerId = "bootstrap-container";
        private const string LoadingLabelId = "loading-label";
        private const string AppNameLabelId = "app-name-label";

        private IBootstrapUIViewModel _viewModel;
        private VisualElement _container;
        private Label _appName;
        private Label _loadingLabel;

        private readonly CompositeDisposable _disposables = new();

        [Inject]
        private void Construct(IBootstrapUIViewModel viewModel) => _viewModel = viewModel;

        private void Start()
        {
            Helper.CheckOnNull(_viewModel, "ViewModel", name);

            var root = gameObject.GetComponent<UIDocument>().rootVisualElement ??
                       throw new NullReferenceException("RootVisualElement is null.");

            _container = root.GetVisualElement<VisualElement>(BootstrapContainerId, name);

            _appName = root.GetVisualElement<Label>(AppNameLabelId, name);
            _appName.text = ProjectConstant.AppName;

            _loadingLabel = root.GetVisualElement<Label>(LoadingLabelId, name);

            _viewModel.TitleText.Subscribe(SetTitle).AddTo(_disposables);
            _viewModel.Opacity.Subscribe(SetOpacity).AddTo(_disposables);
            _viewModel.OnClear.Subscribe(Clear).AddTo(_disposables);
        }

        private void Clear(Unit _)
        {
            _loadingLabel.text = "";
            _appName.text = "";
        }

        private void SetTitle(string value) => _loadingLabel.text = !string.IsNullOrEmpty(value) ? value : "Not set";
        private void SetOpacity(float value) => _container.style.opacity = new StyleFloat(value);
        private void OnDestroy() => _disposables?.Dispose();
    }
}
