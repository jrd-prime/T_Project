using Code.Core.Data;
using UnityEngine;

namespace Code.Core.SO
{
    /// <summary>
    /// Main Settings. Add to the RootContext prefab
    /// </summary>
    [CreateAssetMenu(
        fileName = nameof(MainSettings),
        menuName = SOPathConst.Settings + nameof(MainSettings),
        order = 100)]
    public class MainSettings : SettingsBase
    {
    }
}
