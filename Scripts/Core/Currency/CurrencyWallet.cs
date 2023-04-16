using UnityEngine;
using AYellowpaper.SerializedCollections;
using System.Collections.Generic;

namespace Game.Core
{
    [CreateAssetMenu(menuName = "SO/Wallet", fileName = "Wallet_")]
    public class CurrencyWallet : ScriptableObject
    {
        [field: SerializeField] public List<CurrencyData> CurrencyInWallet { get; private set; }

        public void Setup()
        {
            foreach (var item in CurrencyInWallet)
                item.Setup();
        }

        public bool IsEnaughtCurrency(SerializedDictionary<CurrencyData, float> priceDictionary)
        {
            bool result = true;

            foreach (var item in priceDictionary)
                if (!item.Key.IsEnaughtCurrency(item.Value))
                {
                    Debug.Log($"Not enaught {item.Key.Name} - {item.Value - item.Key.Value}");
                    result = false;
                }

            return result;
        }

        public void CurrencyAdd(CurrencyData currency, float amount)
        {
            if (CurrencyInWallet.Contains(currency))
                currency.CurrencyAdd(amount);
            else
                Debug.Log($"Currency {currency.Name} does not exist in wallet {name}.");
        }

        public void CurrencyAdd(SerializedDictionary<CurrencyData, float> currencyDictionary)
        {
            foreach (var item in currencyDictionary)
                CurrencyAdd(item.Key, item.Value);
        }

        public void CurrencySubtraction(CurrencyData currency, float amount)
        {
            if (CurrencyInWallet.Contains(currency))
                currency.CurrencyAdd(-amount);
            else
                Debug.Log($"Currency {currency.Name} does not exist in wallet {name}.");
        }

        public void CurrencySubtraction(SerializedDictionary<CurrencyData, float> currencyDictionary)
        {
            foreach (var item in currencyDictionary)
                CurrencySubtraction(item.Key, item.Value);
        }
    }
}