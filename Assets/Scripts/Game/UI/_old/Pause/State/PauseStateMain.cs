using System;
using Game.UI._old.Base;

namespace Game.UI._old.Pause.State
{
    public sealed class PauseStateMain : UISubStateBase
    {
        public PauseStateMain(UISubStateBaseData data, Enum defaultSubState) : base(data, defaultSubState)
        {
        }
    }

    public class UISubStateBase
    {
        protected UISubStateBase(UISubStateBaseData data, Enum defaultSubState)
        {
            throw new NotImplementedException();
        }
    }
}
