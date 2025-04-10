using Game.Currency;
using UnityEngine;

namespace Data.Currency
{
    [CreateAssetMenu(fileName = nameof(CurrencyData), menuName = "Data/" + nameof(CurrencyData))]
    public sealed class CurrencyData : SettingsBase, ICurrency
    {
        public override string ConfigName => nameof(CurrencyData);
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public string LocalizationKey { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public ECurrencyRarity Rarity { get; private set; }
        [field: SerializeField] public ECurrencyType Type { get; private set; }
        [field: SerializeField] public int MaxStackSize { get; private set; }
    }
}
