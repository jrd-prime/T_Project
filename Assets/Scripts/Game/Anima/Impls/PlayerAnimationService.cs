using System;
using Core.Animations.Interfaces;
using Core.Character.Player.Interfaces;
using Cysharp.Threading.Tasks;
using Game.Extensions;
using UnityEngine;

namespace Game.Anima.Impls
{
    public sealed class PlayerAnimationService : IPlayerAnimationService
    {
        private readonly IPlayer _player;

        public PlayerAnimationService(IPlayer player) => _player = player;

        public void AnimateWithTrigger(string triggerName, string animationStateName, Action onAnimationComplete)
        {
            var animator = _player.Animator as Animator;
            if (!animator) throw new NullReferenceException("Animator is null");
            animator.SetTrigger(triggerName);
            animator.WaitForAnimationCompleteAsync(animationStateName, onAnimationComplete).Forget();
        }
    }
}
