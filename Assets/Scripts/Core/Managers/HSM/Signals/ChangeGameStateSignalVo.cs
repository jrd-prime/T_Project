using System;

namespace Core.Managers.HSM.Signals
{
    public record ChangeGameStateSignalVo(Type StateType)
    {
        public Type StateType { get; private set; } = StateType;
    }
}
