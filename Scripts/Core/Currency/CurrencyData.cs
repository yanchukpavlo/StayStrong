using System;
using Game.Systems.Save;
using UnityEngine;

namespace Game.Core
{
    [CreateAssetMenu(menuName = "SO/Currency", fileName = "Currency_")]
    public class CurrencyData : VariableFloat, ISaveable
    {
        [field: SerializeField, Min(0)] public float Valueble { get; private set; } = 1f;

        public bool IsEnaughtCurrency(float amount)
        {
            return Value >= amount;
        }

        public int EnaughtCurrencyFor(float amountPerOne)
        {
            return Mathf.CeilToInt(Value / amountPerOne);
        }

        public int CompareGiveAmount(CurrencyData take)
        {
            return Mathf.CeilToInt(take.Valueble / Valueble);
        }

        public int CompareGetAmount(CurrencyData take)
        {
            return Mathf.CeilToInt(Valueble / take.Valueble);
        }
    }
}