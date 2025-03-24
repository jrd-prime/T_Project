using Db.Data;

namespace Core.Character.Models
{
    public sealed class PlayerModel
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
    }
}
