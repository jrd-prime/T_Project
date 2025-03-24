namespace Core.Character.Interfaces
{
    public interface ICharacter : IMovable
    {
        int Id { get; }
        string Name { get; }
        string Description { get; }
        int Health { get; }
        int MaxHealth { get; }
    }
}
