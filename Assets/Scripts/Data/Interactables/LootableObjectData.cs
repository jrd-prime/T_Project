using UnityEngine;

namespace Data.Interactables
{
    [CreateAssetMenu(menuName = "Databases/" + nameof(LootableObjectData), fileName = nameof(LootableObjectData))]
    public class LootableObjectData : InteractableSettings
    {
        public override string ConfigName => nameof(LootableObjectData);
    }
}
