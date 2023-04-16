using System.Linq;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using Game.Core;
using Game.Core.UI;

namespace Game.Systems.Occasion
{
    [CreateAssetMenu(menuName = "SO/Occasion/Refugees", fileName = "OccasionRefugees_")]
    public class OccasionRefugees : Occasion
    {
        [SerializeField, Space] VariableInt peopleVariable;
        [SerializeField] VariableFloat stressVariable;
        [SerializeField] int income;
        [SerializeField] int fine;
        [SerializeField] SerializedDictionary<CurrencyData, float> priceDictionary = new();
        [SerializeField] UICurrency currencyPrefab;

        public override string GetDescription()
        {
            return string.Format(description, income, fine);
        }

        public override void GetCondition(Transform conditionRoot)
        {
            UICurrency[] uiCurrency = new UICurrency[priceDictionary.Count];

            for (var i = 0; i < priceDictionary.Count; i++)
                uiCurrency[i] = Instantiate(currencyPrefab, conditionRoot);

            KeyValuePair<CurrencyData, float> dictionaryElement;
            for (int i = 0; i < priceDictionary.Count; i++)
            {
                dictionaryElement = priceDictionary.ElementAt(i);
                uiCurrency[i].Setup(dictionaryElement.Key, dictionaryElement.Value);
            }
        }

        protected override bool IsCanBeAccepted()
        {
            return (DataManager.Wallet.IsEnaughtCurrency(priceDictionary));
        }

        protected override void Done()
        {
            peopleVariable.AddingValue(income);
            DataManager.Wallet.CurrencySubtraction(priceDictionary);
            base.Done();
        }

        public override void Deny()
        {
            stressVariable.AddingValue(fine);
            base.Deny();
        }
    }
}