using UnityEngine;

namespace Db
{
    public abstract class SettingsBase : ScriptableObject
    {
        public abstract string ConfigName { get; }
    }
}
