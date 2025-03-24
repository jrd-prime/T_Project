using System;
using Code.Core.Systems;
using R3;
using UnityEngine;
using Zenject;

namespace Code.Core.Managers.Camera
{
    namespace _Game._Scripts.Framework.Manager.JCamera
    {
        public interface ICameraManager
        {
            public void SetTarget(IFollowable target);
            public void RemoveTarget();
            public UnityEngine.Camera GetMainCamera();
            public Vector3 GetCamEulerAngles();
            public Quaternion GetCamRotation();
        }

        public sealed class CameraManager : CameraManagerBase, IInitializable
        {
            [SerializeField] private Vector3 cameraOffset = new(0, 10, -5);
            private UnityEngine.Camera mainCamera;

            private IFollowable _targetModel;
            private readonly CompositeDisposable _disposables = new();

            public void Initialize()
            {
            }

            private void Start()
            {
                mainCamera = UnityEngine.Camera.main;
                if (!mainCamera) throw new NullReferenceException($"MainCamera is null. {this}");
                mainCamera.transform.position = cameraOffset;
            }

            private void SetCameraPosition(Vector3 position)
            {
                var newPosition = position + cameraOffset;
                if (mainCamera.transform.position == newPosition) return;
                mainCamera.transform.position = newPosition;
            }

            public override void SetTarget(IFollowable target)
            {
                if (target == null) throw new ArgumentNullException($"Target is null. {this}");
                if (_targetModel == target) return;

                _disposables.Clear();
                _targetModel = target;
                _targetModel.Position.Subscribe(SetCameraPosition).AddTo(_disposables);
            }

            public override void RemoveTarget()
            {
                _targetModel = null;
                _disposables?.Dispose();
            }

            public override UnityEngine.Camera GetMainCamera() => mainCamera;
            public override Vector3 GetCamEulerAngles() => mainCamera.transform.eulerAngles;
            public override Quaternion GetCamRotation() => mainCamera.transform.rotation;
            private void OnDestroy() => _disposables?.Dispose();
        }
    }
}
