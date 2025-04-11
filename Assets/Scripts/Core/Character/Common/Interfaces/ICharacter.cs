using Game.Gameplay.Character.Player.Impls;

namespace Core.Character.Common.Interfaces
{
    public interface ICharacter : ICommandExecutor, IMovable
    {
        string Id { get; }
        string Name { get; }
        string Description { get; }
        object Animator { get; }
        int Health { get; }
        int MaxHealth { get; }
    }
}
