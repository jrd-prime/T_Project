using UnityEngine;

namespace Data
{
    public abstract class SettingsBase : ScriptableObject
    {
        public abstract string ConfigName { get; }
    }
}
