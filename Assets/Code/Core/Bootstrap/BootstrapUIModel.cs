using System;
using Code.Core.Data;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using R3;

namespace Code.Core.Bootstrap
{
    public interface IBootstrapUIModel : IDisposable
    {
        public ReactiveProperty<string> LoadingText { get; }
        public ReactiveProperty<float> Opacity { get; }
        public void SetLoadingText(string value);
        public UniTask FadeOut(float durationInSeconds = 1f);
    }

    [UsedImplicitly]
    public sealed class BootstrapUIModel : IBootstrapUIModel
    {
        public ReactiveProperty<string> LoadingText { get; } = new();
        public ReactiveProperty<float> Opacity { get; } = new(ProjectConstant.UIToolkitOpacityMaxValue);

        public void SetLoadingText(string value) =>
            LoadingText.Value = !string.IsNullOrEmpty(value) ? value : "Not set";

        public async UniTask FadeOut(float durationInSeconds = 1f)
        {
            const float fadeStep = 0.01f;
            var tickDelay = durationInSeconds / (ProjectConstant.UIToolkitOpacityMaxValue / fadeStep);

            while (Opacity.Value > 0)
            {
                Opacity.Value -= fadeStep;
                await UniTask.WaitForSeconds(tickDelay);
            }
        }

        public void Dispose() => LoadingText?.Dispose();
    }
}
