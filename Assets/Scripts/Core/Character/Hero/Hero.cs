using System;
using R3;
using UnityEngine;
using Zenject;

namespace Core.Character.Hero
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(Collider))]
    public sealed class Hero : MonoBehaviour
    {
        private IHeroModel _model;

        private Animator _animator;
        private Collider _collider;
        private Rigidbody _rigidbody;

        private readonly CompositeDisposable _disposables = new();

        [Inject]
        private void Construct(IHeroModel model) => _model = model;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider>();
        }

        private void Start()
        {
            if (_model == null) throw new NullReferenceException("Model is null. " + name);
        }

        private void SetPosition(Vector3 position) => _rigidbody.position = position;
        private void SetRotation(Quaternion rotation) => _rigidbody.rotation = rotation;


        private void OnDestroy() => _disposables?.Dispose();
    }
}
