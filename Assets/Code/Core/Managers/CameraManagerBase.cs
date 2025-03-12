using Code.Core.Managers._Game._Scripts.Framework.Manager.JCamera;
using UnityEngine;

namespace Code.Core.Managers
{
    public abstract class CameraManagerBase : MonoBehaviour, ICameraManager
    {
        public abstract void SetTarget(IFollowable target);

        public abstract void RemoveTarget();

        public abstract Camera GetMainCamera();

        public abstract Vector3 GetCamEulerAngles();

        public abstract Quaternion GetCamRotation();
    }
}
