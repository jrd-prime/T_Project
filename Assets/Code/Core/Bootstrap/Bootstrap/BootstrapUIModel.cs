using System;
using R3;

namespace Code.Core.Bootstrap.Bootstrap
{
    public interface IBootstrapUIModel : IDisposable
    {
        public ReactiveProperty<string> LoadingText { get; }
        public void SetLoadingText(string value);
    }

    public class BootstrapUIModel : IBootstrapUIModel
    {
        public ReactiveProperty<string> LoadingText { get; } = new();

        public void SetLoadingText(string value) =>
            LoadingText.Value = !string.IsNullOrEmpty(value) ? value : "Not set";

        public void Dispose() => LoadingText?.Dispose();
    }
}
