using Core.Character.Player.Models;

namespace Core.Character.Player.Services
{
    public sealed class PlayerService
    {
        private readonly PlayerModel _model;

        public PlayerService(PlayerModel model) => _model = model;
    }
}
