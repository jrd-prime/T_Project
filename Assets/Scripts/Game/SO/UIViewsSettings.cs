using Db.Data;
using UnityEngine;

namespace Game.SO
{
    [CreateAssetMenu(
        fileName = nameof(UIViewsSettings),
        menuName = SOPathConst.MainSettings + nameof(UIViewsSettings),
        order = 100)]
    public class UIViewsSettings : SettingsBase
    {
        [field: SerializeField] public UIStateData[] States { get; private set; }
    }
}
