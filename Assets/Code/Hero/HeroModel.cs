using Code.Core.Input;
using Code.Core.Managers;
using Code.Core.Systems;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Hero
{
    internal interface IHeroModel : IFollowable
    {
    }

    public sealed class HeroModel : IHeroModel, IInitializable, IFixedTickable
    {
        public ReactiveProperty<Vector3> Position { get; } = new(Vector3.zero);
        public ReactiveProperty<Quaternion> Rotation { get; } = new(Quaternion.identity);

        private IObjectResolver _resolver;
        private IInput _input;

        private Vector3 _direction;

        [Inject]
        private void Construct(IObjectResolver resolver) => _resolver = resolver;

        public void Initialize()
        {
            var cameraFollowSystem = _resolver.Resolve<CameraFollowSystem>();
            cameraFollowSystem.SetTarget(this);

            _input = _resolver.Resolve<IInput>();
            _input.MoveDirection.Subscribe(SetDirection);
        }

        private void SetDirection(Vector3 direction) => _direction = direction;

        public void FixedTick()
        {
            Position.Value += _direction * Time.fixedDeltaTime * 10f;
        }
    }
}
