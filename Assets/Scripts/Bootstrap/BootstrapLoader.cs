using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace Bootstrap
{
    public interface IBootable
    {
        public UniTask InitializationOnBoot();
        public string Description { get; }
    }

    public sealed class BootstrapLoader : IInitializable
    {
        private IBootstrapUIModel _bootstrapUIModel;
        private readonly Queue<IBootable> _loadingQueue = new();

        [Inject]
        private void Construct(IBootstrapUIModel bootstrapUIModel) => _bootstrapUIModel = bootstrapUIModel;

        public void Initialize()
        {
            if (_bootstrapUIModel == null) throw new NullReferenceException($"BootstrapUIModel is null. {this}");
        }

        public void AddForBootstrapInitialization(IBootable bootable) => _loadingQueue.Enqueue(bootable);

        public async UniTask StartServicesInitializationAsync(int pseudoDelay = 0)
        {
            if (_loadingQueue.Count == 0)
                throw new Exception("No services to initialize! Use AddServiceForInitialization first.");

            foreach (var service in _loadingQueue)
            {
                try
                {
                    _bootstrapUIModel.SetLoadingText($"Loading: {service.Description}..");
                    await service.InitializationOnBoot();

                    if (pseudoDelay > 0) await UniTask.Delay(pseudoDelay); // fake delay per service
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to initialize {service.GetType().Name}: {ex.Message}");
                }
            }

            await UniTask.CompletedTask;
        }
    }
}
