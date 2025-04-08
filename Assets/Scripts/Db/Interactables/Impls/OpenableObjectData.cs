using UnityEngine;

namespace Db.Interactables.Impls
{
    [CreateAssetMenu(menuName = "Databases/" + nameof(OpenableObjectData), fileName = nameof(OpenableObjectData))]
    public class OpenableObjectData : DataBase<OpenableObjectVo>
    {
        public override string ConfigName => nameof(OpenableObjectData);
    }
}
