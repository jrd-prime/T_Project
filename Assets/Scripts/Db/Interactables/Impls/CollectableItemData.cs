using UnityEngine;

namespace Db.Interactables.Impls
{
    [CreateAssetMenu(menuName = "Databases/" + nameof(CollectableItemData), fileName = nameof(CollectableItemData))]
    public class CollectableItemData : DataBase<CollectableItemVo>
    {
        public override string ConfigName => nameof(CollectableItemData);
    }
}
