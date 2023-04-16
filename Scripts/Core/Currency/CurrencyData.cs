using System;
using Game.Systems.Save;
using UnityEngine;

namespace Game.Core
{
    [CreateAssetMenu(menuName = "SO/Currency", fileName = "Currency_")]
    public class CurrencyData : ScriptableObject, ISaveable
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public float Value { get; private set; }
        [field: SerializeField] public float StartValue { get; private set; }
        [field: SerializeField] public string Name = "Currency_name";
        [field: SerializeField, Min(0)] public float Valueble { get; private set; } = 1f;
        [field: SerializeField] public Sprite Icon { get; private set; } = null;

        public event Action<float> OnValueUpdate;

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(Id))
                Id = ISaveable.GetId();
        }

        public void Setup()
        {
            Value = StartValue;
            ISaveable.Saveables[Id] = this;
        }

        public bool IsEnaughtCurrency(float amount)
        {
            return Value >= amount;
        }

        public int EnaughtCurrencyFor(float amountPerOne)
        {
            return Mathf.CeilToInt(Value / amountPerOne);
        }

        public void CurrencyAdd(float amount)
        {
            if (!amount.IsZero())
                UpdateValue(Value + amount);
        }

        public int CompareGiveAmount(CurrencyData take)
        {
            return Mathf.CeilToInt(take.Valueble / Valueble);
        }

        public int CompareGetAmount(CurrencyData take)
        {
            return Mathf.CeilToInt(Valueble / take.Valueble);
        }

        void UpdateValue(double newValue)
        {
            newValue = Math.Round(newValue, 3);

            if (newValue != Value)
            {
                Value = (float)newValue;
                if (Value < 0)
                    Value = 0;

                OnValueUpdate?.Invoke(Value);
            }
        }

        public object SaveState()
        {
            return Value;
        }

        public void LoadState(object data)
        {
            UpdateValue((float)data);
        }
    }
}