using System;
using Data.Currency;

namespace Data.SO
{
    [Serializable]
    public struct DropVo
    {
        public CurrencyData currency;
        public float min;
        public float max;
    }
}