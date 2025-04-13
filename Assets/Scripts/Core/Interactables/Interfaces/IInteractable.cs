using Core.Character.Common.Interfaces;
using Cysharp.Threading.Tasks;

namespace Core.Interactables.Interfaces
{
    public interface IInteractable
    {
        bool CanInteract { get; }
        string InteractionTipNameId { get; }
        string LocalizationKey { get; }
        UniTask InteractAsync(ICharacter character);
    }
}
