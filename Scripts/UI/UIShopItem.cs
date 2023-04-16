using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Systems.Building;

namespace Game.Core.UI
{
    public class UIShopItem : MonoBehaviour
    {
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] UICurrency currencyPrefab;
        [SerializeField] UIVariableVisual uiVatiablePrefab;
        [SerializeField] Transform priceRoot;
        [SerializeField] Transform modificatorRoot;

        [Header("Events")]
        [SerializeField] GameEvent eventStartBuild;

        List<UICurrency> uICurrency = new List<UICurrency>();
        StructureData structureData;

        public void Setup(StructureData data)
        {
            structureData = data;

            icon.sprite = data.Icon;
            nameText.text = data.Name;

            UpdatePrice();
            UpdateModificator();
        }

        public void BayItem()
        {
            if (!DataManager.Wallet.IsEnaughtCurrency(structureData.PriceDictionary))
                return;

            eventStartBuild.Invoke(this, structureData);
        }

        void UpdatePrice()
        {
            foreach (var item in uICurrency)
                item.gameObject.SetActive(false);

            int notEnough = structureData.PriceDictionary.Count - uICurrency.Count;
            for (var i = 0; i < notEnough; i++)
            {
                uICurrency.Add(Instantiate(currencyPrefab, priceRoot));
            }

            KeyValuePair<CurrencyData, float> dictionaryElement;
            for (int i = 0; i < structureData.PriceDictionary.Count; i++)
            {
                dictionaryElement = structureData.PriceDictionary.ElementAt(i);
                uICurrency[i].Setup(dictionaryElement.Key, dictionaryElement.Value);
                uICurrency[i].gameObject.SetActive(true);
            }
        }

        void UpdateModificator()
        {
            foreach (var item in structureData.IncomeDictionary)
            {
                Instantiate(uiVatiablePrefab, modificatorRoot).Setup(item.Key.Value, item.Value.ToString());
            }
        }
    }
}