﻿using Db.Data;
using UnityEngine;

namespace Db.SO
{
    [CreateAssetMenu(
        fileName = nameof(HeroSettings),
        menuName = SOPathConst.MainSettings + nameof(HeroSettings),
        order = 100)]
    public class HeroSettings : SettingsBase
    {
        [field: SerializeField] public float Speed { get; private set; } = 5f;
        [field: SerializeField] public float RotationSpeed { get; private set; } = 5f;
    }
}
