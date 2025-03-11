using Code.Core;
using Code.Core.Input;
using Code.Core.Managers;
using Code.Core.SO;
using Code.Core.Systems;
using Code.Extensions;
using JetBrains.Annotations;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Code.Hero
{
    public interface IHeroModel : IFollowable
    {
    }

    [UsedImplicitly]
    public sealed class HeroModel : ModelBase, IHeroModel, IInitializable, IFixedTickable
    {
        public ReadOnlyReactiveProperty<Vector3> Position { get; private set; }
        public ReadOnlyReactiveProperty<Quaternion> Rotation { get; private set; }

        private readonly ReactiveProperty<Vector3> _position = new(Vector3.zero);
        private readonly ReactiveProperty<Quaternion> _rotation = new(Quaternion.identity);

        private IInput _input;
        private Vector3 _direction;
        private HeroSettings _heroSettings;

        public void Initialize()
        {
            Position = _position.ToReadOnlyReactiveProperty();
            Rotation = _rotation.ToReadOnlyReactiveProperty();

            var cameraFollowSystem = Resolver.ResolveAndCheckOnNull<CameraFollowSystem>();
            cameraFollowSystem.SetTarget(this);

            _heroSettings = SettingsProvider.GetSettings<HeroSettings>();

            _input = Resolver.ResolveAndCheckOnNull<IInput>();
            _input.MoveDirection.Subscribe(SetDirection).AddTo(Disposables);
        }

        private void SetDirection(Vector3 direction) => _direction = direction;

        public void FixedTick()
        {
            _position.Value += _direction * Time.fixedDeltaTime * _heroSettings.Speed;
        }
    }
}
