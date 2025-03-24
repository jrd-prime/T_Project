using UnityEngine;
using Zenject;

namespace Installers
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.LogWarning("UIInstaller.InstallBindings");
        }
    }
}
