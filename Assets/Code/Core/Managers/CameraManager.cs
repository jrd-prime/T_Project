using System;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Code.Core.Managers
{
    namespace _Game._Scripts.Framework.Manager.JCamera
    {
        public interface ICameraManager
        {
            public void SetTarget(IFollowable target);
            public void RemoveTarget();
            public Camera GetMainCamera();
            public Vector3 GetCamEulerAngles();
            public Quaternion GetCamRotation();
        }

        public sealed class CameraManager : CameraManagerBase, IInitializable
        {
            [SerializeField] private Vector3 cameraOffset = new(0, 10, -5);
            [SerializeField] private Camera mainCamera;

            private IFollowable _targetModel;
            private readonly CompositeDisposable _disposables = new();

            public void Initialize()
            {
                if (!mainCamera) throw new NullReferenceException($"MainCamera is null. {this}");
                SetCameraPosition(cameraOffset);
            }

            private void SetCameraPosition(Vector3 position)
            {
                if (mainCamera.transform.position == position) return;
                mainCamera.transform.position = position + cameraOffset;
            }

            public override void SetTarget(IFollowable target)
            {
                if (target == null) throw new ArgumentNullException($"Target is null. {this}");

                SetCameraPosition(target.Position.Value);

                if (_targetModel != null) ResubscribeToNewTarget(target);
            }

            private void ResubscribeToNewTarget(IFollowable target)
            {
                _disposables?.Dispose();
                SubscribeToTargetPosition(target);
            }

            public override void RemoveTarget()
            {
                _targetModel = null;
                _disposables?.Dispose();
            }

            public override Camera GetMainCamera() => mainCamera;
            public override Vector3 GetCamEulerAngles() => mainCamera.transform.eulerAngles;
            public override Quaternion GetCamRotation() => mainCamera.transform.rotation;

            private void SubscribeToTargetPosition(IFollowable target)
            {
                _targetModel = target;
                _targetModel.Position.Subscribe(SetCameraPosition).AddTo(_disposables);
            }

            private void OnDestroy() => _disposables?.Dispose();
        }
    }
}
