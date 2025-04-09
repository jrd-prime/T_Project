using UnityEngine;

namespace Db.Interactables
{
    [CreateAssetMenu(menuName = "Databases/" + nameof(OpenableObjectData), fileName = nameof(OpenableObjectData))]
    public class OpenableObjectData : InteractableSettings
    {
        public override string ConfigName => nameof(OpenableObjectData);
    }
}
