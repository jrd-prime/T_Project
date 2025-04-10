using System;
using Data.Currency;
using UnityEngine;

namespace Data.Interactables
{
    [CreateAssetMenu(menuName = "Databases/" + nameof(GatherableObjectData), fileName = nameof(GatherableObjectData))]
    public class GatherableObjectData : InteractableSettings
    {
        public ReturnsVo[] returns;
        public RequirementsVo requirements;
        public override string ConfigName => nameof(GatherableObjectData);
    }

    [Serializable]
    public struct RequirementsVo
    {
    }

    [Serializable]
    public struct ReturnsVo
    {
        public CurrencyData currency;
        public float min;
        public float max;
    }
}
