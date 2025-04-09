using Core.Character.Common.Interfaces;
using UnityEngine;

namespace Game.Managers
{
    public interface ICameraManager
    {
        void SetTarget(IFollowable target);
        void RemoveTarget();
        Camera GetMainCamera();
        Vector3 GetCamEulerAngles();
        Quaternion GetCamRotation();
    }
}
