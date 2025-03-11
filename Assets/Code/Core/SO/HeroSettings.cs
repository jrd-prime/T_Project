using Code.Core.Data;
using UnityEngine;

namespace Code.Core.SO
{
    [CreateAssetMenu(
        fileName = nameof(HeroSettings),
        menuName = SOPathConst.MainSettings + nameof(HeroSettings),
        order = 100)]
    public class HeroSettings : SettingsBase
    {
        [field: SerializeField] public float Speed { get; private set; } = 5f;
    }
}
