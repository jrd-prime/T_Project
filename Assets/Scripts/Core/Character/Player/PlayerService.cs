using Core.Data;

namespace Core.Character.Player
{
    public sealed class PlayerService
    {
        private readonly PlayerModel _model;

        public PlayerService(PlayerModel model) => _model = model;

        public void SetPosition(JVector3 position) => _model.Position = position;
    }
}
