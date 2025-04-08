using Core.Managers.HSM.Signals;
using Core.Managers.UI.Signals;
using Game.UI.Impls;
using Infrastructure.Input;
using Infrastructure.Input.Signals;
using Infrastructure.Input.Signals.Keys;
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
            Container.DeclareSignal<ShowViewSignalVo>();
            Container.DeclareSignal<ShowPreviousViewSignalVo>();
            // Input
            Container.DeclareSignal<EnableInputSignal>();
            Container.DeclareSignal<DisableInputSignal>();
            Container.DeclareSignal<MoveDirectionSignal>();
            // Key
            Container.DeclareSignal<EscapeKeySignal>();
            Container.DeclareSignal<InventoryKeySignal>();
            Container.DeclareSignal<InteractKeySignal>();
            // UI
            Container.DeclareSignal<ShowInteractPromptSignal>();
            Container.DeclareSignal<HideInteractPromptSignal>();
        }
    }
}
