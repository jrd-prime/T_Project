using System;

namespace Core.Animations.Interfaces
{
    public interface IPlayerAnimationService
    {
        void AnimateWithTrigger(string triggerName, string animationStateName, Action onAnimationComplete);
    }
}
