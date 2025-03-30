using System;
using Db.Data;
using UnityEngine;

namespace Core.Character.Player
{
    public sealed class Player : MonoBehaviour, IPlayer
    {
        
        public void Move(Position position)
        {
            throw new NotImplementedException();
        }

        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public int Health { get; }
        public int MaxHealth { get; }
    }
}
