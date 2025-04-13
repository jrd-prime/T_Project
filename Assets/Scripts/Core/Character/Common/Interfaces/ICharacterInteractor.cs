using System;

namespace Core.Character.Common.Interfaces
{
    public interface ICharacterInteractor
    {
        void AnimateWithTrigger(string triggerName, string animationStateName, Action onAnimationComplete);
    }
}
