using UnityEngine;

namespace Game.Core.UI.Windows
{
    public class WindowTrade : Window
    {
        [System.Serializable]
        public class TradeContent
        {
            [field: SerializeField] public int[] Amounts { get; private set; }
            [field: SerializeField] public CurrencyData[] Give { get; private set; }
            [field: SerializeField] public CurrencyData[] Get { get; private set; }
        }

        [SerializeField] UITradeItem itemPrefab;
        [SerializeField] Transform itemsRoot;
        [SerializeField] TradeContent content;

        private void Awake()
        {
            UpdateTrade();
        }

        public void UpdateTrade()
        {
            CurrencyWallet wallet = DataManager.Wallet;

            foreach (var give in content.Give)
            {
                if (!wallet.CurrencyInWallet.Contains(give))
                    continue;

                foreach (var get in content.Get)
                {
                    if (get.Equals(give) || !wallet.CurrencyInWallet.Contains(get))
                        continue;

                    foreach (var amount in content.Amounts)
                    {
                        Instantiate(itemPrefab, itemsRoot).Setup(give, get, amount);
                    }
                }
            }
        }
    }
}