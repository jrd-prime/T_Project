using Core.Extensions;
using Db.SO;
using Game.Systems;
using Infrastructure.Input;
using JetBrains.Annotations;
using R3;
using UnityEngine;
using Zenject;

namespace Core.Character.Hero
{
    public interface IHeroModel : IFollowable
    {
        public ReadOnlyReactiveProperty<float> Health { get; }
    }

    [UsedImplicitly]
    public sealed class HeroModel : ModelBase, IHeroModel, IInitializable, IFixedTickable
    {
        public ReadOnlyReactiveProperty<Vector3> Position { get; private set; }
        public ReadOnlyReactiveProperty<Quaternion> Rotation { get; private set; }
        public ReadOnlyReactiveProperty<float> Health { get; private set; }

        private readonly ReactiveProperty<Vector3> _position = new(Vector3.zero);
        private readonly ReactiveProperty<Quaternion> _rotation = new(Quaternion.identity);
        private readonly ReactiveProperty<float> _health = new(100);

        private IJInput _ijInput;
        private Vector3 _direction;
        private HeroSettings _heroSettings;

        public void Initialize()
        {
            Position = _position.ToReadOnlyReactiveProperty();
            Rotation = _rotation.ToReadOnlyReactiveProperty();
            Health = _health.ToReadOnlyReactiveProperty();

            var cameraFollowSystem = Container.ResolveAndCheckOnNull<CameraFollowSystem>();
            cameraFollowSystem.SetTarget(this);

            _heroSettings = SettingsProvider.GetSettings<HeroSettings>();

            _ijInput = Container.ResolveAndCheckOnNull<IJInput>();
            _ijInput.MoveDirection.Subscribe(SetDirection).AddTo(Disposables);
        }

        public void FixedTick()
        {
            _position.Value += _direction * Time.fixedDeltaTime * _heroSettings.Speed;

            if (_direction == Vector3.zero) return;

            _rotation.Value = Quaternion.Slerp(_rotation.Value, Quaternion.LookRotation(_direction),
                Time.fixedDeltaTime * _heroSettings.RotationSpeed);
        }

        private void SetDirection(Vector3 direction) => _direction = direction;
    }
}
