using UnityEngine;

namespace Db.Interactables
{
    [CreateAssetMenu(menuName = "Databases/" + nameof(GatherableObjectData), fileName = nameof(GatherableObjectData))]
    public class GatherableObjectData : InteractableSettings
    {
        public override string ConfigName => nameof(GatherableObjectData);
    }
}
