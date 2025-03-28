﻿using R3;
using UnityEngine;

namespace Code.Core.Input
{
    public interface IJInput
    {
        public Observable<Vector3> MoveDirection { get; }
        public Observable<Unit> OnEscape { get; }
        public Observable<Unit> OnInventory { get; }
        public Observable<Unit> OnQuestLog { get; }
    }
}
