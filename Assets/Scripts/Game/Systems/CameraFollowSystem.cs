using System;
using Core.Managers.Camera._Game._Scripts.Framework.Manager.JCamera;
using ModestTree;
using R3;
using UnityEngine;
using Zenject;

namespace Game.Systems
{
    public interface IFollowable
    {
        public ReactiveProperty<Vector3> Position { get; }
    }

    public sealed class CameraFollowSystem
    {
        private readonly ICameraManager _cameraManager;
        private bool _hasTarget;
        public CameraFollowSystem(ICameraManager cameraManager) => _cameraManager = cameraManager;

        public void SetTarget(IFollowable target)
        {
            Log.Warn("CameraFollowSystem.SetTarget");
            if (_cameraManager == null)
                throw new NullReferenceException("CameraManager is null " + nameof(CameraFollowSystem));
            if (target == null) throw new NullReferenceException("Target is null " + nameof(CameraFollowSystem));

            // TODO remove this
            if (_hasTarget)
            {
                Debug.LogWarning("CameraFollowSystem already has target. Removing...");
                _cameraManager.RemoveTarget();
                _hasTarget = false;
            }
            else
            {
                _cameraManager.SetTarget(target);
                _hasTarget = true;
            }
        }
    }
}
