using Db.Data;
using UnityEngine;

namespace Db.SO
{
    [CreateAssetMenu(
        fileName = nameof(LocalizationSettings),
        menuName = SOPathConst.MainSettings + nameof(LocalizationSettings),
        order = 0)]
    public sealed class LocalizationSettings : SettingsBase
    {
        [field: SerializeField] public Language DefaultLanguage { get; private set; } = Language.English;
    }

    public enum Language
    {
        English = 0,
        Russian = 1
    }
}
