using UnityEngine;

namespace Game.Core
{
    [CreateAssetMenu(menuName = "SO/Modificator/Currency", fileName = "CM_")]
    public class CurrencyModificator : Modificator
    {
        [field: SerializeField] public CurrencyData Currency { get; private set; }
        [field: SerializeField] public VariableFloat AmountPerTickVariable { get; private set; }

        int intTemp;
        float floatTempValue;

        public override void Setup()
        {

        }

        public override void Enable()
        {
            EventsManager.OnTick += UpdateCurrency;
        }

        public override void Disable()
        {
            EventsManager.OnTick -= UpdateCurrency;
        }

        public override void ManualUpdate(int amount)
        {
            DataManager.Wallet.CurrencyAdd(Currency, amount * AmountPerTickVariable.Value);
        }

        void UpdateCurrency()
        {
            if (AmountPerTickVariable.Value.IsZero())
                return;

            intTemp = Mathf.CeilToInt(floatTempValue += AmountPerTickVariable.Value);
            if (intTemp != 0)
            {
                floatTempValue -= intTemp;
                DataManager.Wallet.CurrencyAdd(Currency, intTemp);
            }
        }
    }
}
