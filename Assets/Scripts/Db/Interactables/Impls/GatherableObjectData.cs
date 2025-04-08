using UnityEngine;

namespace Db.Interactables.Impls
{
    [CreateAssetMenu(menuName = "Databases/" + nameof(GatherableObjectData), fileName = nameof(GatherableObjectData))]
    public class GatherableObjectData : DataBase<GatherableObjectVo>
    {
        public override string ConfigName => nameof(GatherableObjectData);
    }
}
