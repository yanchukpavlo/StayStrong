using UnityEngine;

namespace Game.Core.UI
{
    public class UIWallet : MonoBehaviour
    {
        [SerializeField] CurrencyWallet wallet;
        [SerializeField] UICurrency currencyVisual;
        [SerializeField] RectTransform contentHolder;

        private void Awake()
        {
            foreach (var item in wallet.CurrencyInWallet)
                Create(item, item.Value);
        }

        void Create(CurrencyData currency, float starValue)
        {
            var visual = Instantiate(currencyVisual, contentHolder);
            visual.Setup(currency, starValue, true);
        }
    }
}