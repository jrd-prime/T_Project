using System;
using R3;
using VContainer;
using VContainer.Unity;

namespace Code.Core.Bootstrap
{
    public interface IBootstrapUIViewModel : IInitializable
    {
        public ReactiveProperty<string> TitleText { get; }
    }

    public class BootstrapUIViewModel : IBootstrapUIViewModel
    {
        public ReactiveProperty<string> TitleText => _model.LoadingText;

        private IBootstrapUIModel _model;

        [Inject]
        private void Construct(IBootstrapUIModel bootstrapUIModel) => _model = bootstrapUIModel;

        public void Initialize()
        {
            if (_model == null) throw new NullReferenceException($"{typeof(IBootstrapUIModel)} is null.");
        }
    }
}
