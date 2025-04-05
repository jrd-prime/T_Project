using System;
using Core.Character.Player.Interactors;
using Core.Extensions;
using ModestTree;
using R3;
using UnityEngine;
using Zenject;

namespace Core.Character.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class Player : MonoBehaviour, IPlayer
    {
        public ReactiveProperty<Vector3> Position { get; } = new();
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public int Health { get; }
        public int MaxHealth { get; }

        [Inject] private PlayerInteractor _interactor;
        private readonly CompositeDisposable _disposables = new();
        private Rigidbody _rb;
        [SerializeField] private float moveSpeed = 5f; // Скорость перемещения
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private float acceleration = 0.1f;
        private Vector3 currentVelocity;
        private Camera _mainCamera;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _mainCamera = _interactor.MainCamera;
        }

        private void FixedUpdate()
        {
            Vector3 moveDirection = _interactor.MoveDirection;

            if (moveDirection != Vector3.zero)
            {
                // Преобразуем направление относительно камеры
                Vector3 cameraForward = _mainCamera.transform.forward;
                Vector3 cameraRight = _mainCamera.transform.right;

                // Убираем вертикальную составляющую (оставляем только XZ плоскость)
                cameraForward.y = 0f;
                cameraRight.y = 0f;
                cameraForward = cameraForward.normalized;
                cameraRight = cameraRight.normalized;

                // Вычисляем итоговое направление движения
                Vector3 adjustedDirection =
                    (cameraForward * moveDirection.z + cameraRight * moveDirection.x).normalized;

                Vector3 targetVelocity = adjustedDirection * moveSpeed;
                currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, acceleration);

                _rb.linearVelocity = new Vector3(currentVelocity.x, _rb.linearVelocity.y, currentVelocity.z);

                // Плавный поворот в направлении движения
                Quaternion targetRotation = Quaternion.LookRotation(adjustedDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                    rotationSpeed * Time.fixedDeltaTime);
            }
            else
            {
                // Плавная остановка
                currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, acceleration);
                _rb.linearVelocity = new Vector3(currentVelocity.x, _rb.linearVelocity.y, currentVelocity.z);
            }

            Position.Value = transform.position;
        }

        public void Move(Vector3 direction)
        {
            // Можно использовать этот метод для дополнительной логики при изменении направления
            // Например, запуск анимации движения
        }


        private void OnDestroy()
        {
            // Очищаем подписки
            _disposables.Dispose();
        }
    }
}
