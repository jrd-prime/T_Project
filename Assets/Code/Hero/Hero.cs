using System;
using R3;
using UnityEngine;
using VContainer;

namespace Code.Hero
{
    [RequireComponent(typeof(Animator), typeof(Collider))]
    public sealed class Hero : MonoBehaviour
    {
        private Animator _animator;
        private Collider _collider;
        private IHeroModel _model;

        private readonly CompositeDisposable _disposables = new();

        [Inject]
        private void Construct(IHeroModel model) => _model = model;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider>();
        }

        private void Start()
        {
            if (_model == null) throw new NullReferenceException("Model is null. " + name);

            _model.Position.Subscribe(SetPosition).AddTo(_disposables);
        }

        private void SetPosition(Vector3 position) => transform.position = position;


        private void OnDestroy()
        {
            _disposables?.Dispose();
        }
    }
}
