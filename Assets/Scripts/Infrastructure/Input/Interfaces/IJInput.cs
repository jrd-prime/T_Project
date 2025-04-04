using R3;
using UnityEngine;

namespace Infrastructure.Input.Interfaces
{
    public interface IJInput
    {
        Observable<Vector3> MoveDirection { get; }
        Observable<Unit> OnEscape { get; }
        Observable<Unit> OnInventory { get; }
        Observable<Unit> OnQuestLog { get; }
    }
}
