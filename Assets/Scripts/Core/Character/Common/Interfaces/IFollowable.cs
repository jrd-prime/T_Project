using Core.Data;
using R3;

namespace Core.Character.Common.Interfaces
{
    public interface IFollowable
    {
        ReactiveProperty<JVector3> Position { get; }
    }
}
