using UnityEngine;

namespace Game.Core.UI
{
    public class UITradeItem : MonoBehaviour
    {
        [SerializeField] UICurrency currencyFrom;
        [SerializeField] UICurrency currencyTo;

        int giveAmount;
        int getAmount;

        public void Setup(CurrencyData give, CurrencyData take, int amount)
        {
            giveAmount = give.CompareGiveAmount(take) * amount;
            getAmount = give.CompareGetAmount(take) * amount;

            currencyFrom.Setup(give, giveAmount);
            currencyTo.Setup(take, getAmount);
        }

        public void Trade()
        {
            if (!currencyFrom.Currency.IsEnaughtCurrency(giveAmount))
                return;

            DataManager.Wallet.CurrencySubtraction(currencyFrom.Currency, giveAmount);
            DataManager.Wallet.CurrencyAdd(currencyTo.Currency, getAmount);
        }
    }
}