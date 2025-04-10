namespace Core.Character.Common.Interfaces
{
    public interface ICharacter : IMovable
    {
        string Id { get; }
        string Name { get; }
        string Description { get; }
        int Health { get; }
        int MaxHealth { get; }
    }
}
