using Core.Currency;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Data.Currency
{
    [CreateAssetMenu(fileName = nameof(CurrencyData), menuName = "Data/" + nameof(CurrencyData))]
    public sealed class CurrencyData : SettingsBase, ICurrency
    {
        [SerializeField] private string id;
        [SerializeField] private string localizationKey;
        [SerializeField] private string description;

        [SerializeField] [ReadOnly] private string iconId;
        [SerializeField] private ECurrencyRarity rarity;
        [SerializeField] private ECurrencyType type;
        [SerializeField] private int maxStackSize;

        public override string ConfigName => nameof(CurrencyData);

        public string Id => id;
        public string LocalizationKey => localizationKey;
        public string Description => description;
        public string IconId => iconId;
        public ECurrencyRarity Rarity => rarity;
        public ECurrencyType Type => type;
        public int MaxStackSize => maxStackSize;

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(id))
            {
                Debug.LogWarning($"{nameof(CurrencyData)}: ID cannot be empty.", this);
                id = GUID.Generate().ToString(); // Или другое значение по умолчанию
            }

            if (maxStackSize <= 0)
            {
                Debug.LogWarning($"{nameof(CurrencyData)}: MaxStackSize must be positive.", this);
                maxStackSize = 1;
            }

            if (string.IsNullOrEmpty(localizationKey))
            {
                Debug.LogWarning($"{nameof(CurrencyData)}: LocalizationKey is empty.", this);
            }

            iconId = localizationKey;
        }
    }
}
