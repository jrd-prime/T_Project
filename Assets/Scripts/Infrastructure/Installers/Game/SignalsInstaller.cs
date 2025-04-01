using Core.Managers.HSM.Signals;
using Core.Managers.UI.Signals;
using Zenject;

namespace Infrastructure.Installers.Game
{
    public sealed class SignalsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // HSM
            Container.DeclareSignal<ChangeGameStateSignalVo>();
            // UI Manager
            Container.DeclareSignal<SwitchLocalViewSignalVo>();
            Container.DeclareSignal<SwitchToPreviousViewSignalVo>();
        }
    }
}
