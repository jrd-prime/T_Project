using Core.Data;

namespace Core.Character.Player
{
    public sealed class PlayerModel
    {
        public string Id { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public JVector3 Position { get; set; }
    }
}
