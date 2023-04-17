using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.Core.UI
{
    public class UICurrency : MonoBehaviour
    {
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI valueText;

        public CurrencyData Currency => currency;
    
        bool subscribeToUpdate;
        CurrencyData currency;

        private void OnDestroy()
        {
            if (subscribeToUpdate)
                currency.OnValueUpdate -= UpdateText;
        }

        public void Setup(CurrencyData currencyData, float startValue, bool updateByEvent = false)
        {
            subscribeToUpdate = updateByEvent;
            currency = currencyData;

            icon.sprite = currency.Icon;
            UpdateText(startValue, startValue);

            if (subscribeToUpdate)
                currency.OnValueUpdate += UpdateText;
        }

        void UpdateText(float newValue, float old)
        {
            valueText.text = newValue.ToString();
        }
    }
}