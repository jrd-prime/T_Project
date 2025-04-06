using Core.Character.Player.Models;
using UnityEngine;

namespace Core.Character.Player.Services
{
    public sealed class PlayerService
    {
        private readonly PlayerModel _model;

        public PlayerService(PlayerModel model) => _model = model;

        public void SetPosition(Vector3 position) => _model.Position = position;
    }
}
