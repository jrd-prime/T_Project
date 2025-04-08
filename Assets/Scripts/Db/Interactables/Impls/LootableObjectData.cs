using UnityEngine;

namespace Db.Interactables.Impls
{
    [CreateAssetMenu(menuName = "Databases/" + nameof(LootableObjectData), fileName = nameof(LootableObjectData))]
    public class LootableObjectData : DataBase<LootableObjectVo>
    {
        public override string ConfigName => nameof(LootableObjectData);
    }
}
