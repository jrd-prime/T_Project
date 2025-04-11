using Core.Character.Player.Interfaces;
using Cysharp.Threading.Tasks;
using Game.Gameplay.Character.Player.Impls;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Game.Anima
{
    public sealed class GatherCommand : ICommand
    {
        [Inject] private IPlayer _playerInteractor;

        public void Execute()
        {
            Log.Warn("Gather command");
            var animator = _playerInteractor.Animator as Animator;
            if (animator != null) animator.SetTrigger("gather_high");
            Log.Warn("start wait = " + Time.time);
            animator.WaitForAnimationCompleteAsync("gath_an", OnAnimationComplete).Forget();
            Log.Warn("after wait = " + Time.time);
        }

        private void OnAnimationComplete()
        {
            Log.Warn("on animation complete = " + Time.time);
        }
    }
}
