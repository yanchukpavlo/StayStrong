using UnityEngine;
using Game.Utility;

namespace Game.Core
{
    public class DataManager : PersistentSingletone<DataManager>
    {
        public static CurrencyWallet Wallet { get; private set; }
        [field: SerializeField] public VariableInt ClickInfluenceVariable { get; private set; } = null;

        [SerializeField] CurrencyWallet currencyWallet;
        [Space]
        [SerializeField] VariableFloat[] floatVariables;
        [SerializeField] VariableInt[] intVariables;

        [Header("Update in tick")]
        [SerializeField] VariableFloat stressVariable;
        [SerializeField] VariableFloat stressPerTickVariable;
        [SerializeField] Modificator[] modificators;


        protected override void Awake()
        {
            base.Awake();
            Wallet = currencyWallet;
            ResetVariables();
        }

        private void OnEnable()
        {
            EventsManager.OnTick += UpdateInTick;
            Modificator.EnableAll(modificators);
        }

        private void OnDisable()
        {
            EventsManager.OnTick -= UpdateInTick;
            Modificator.DisableAll(modificators);
        }

        public void ResetVariables()
        {
            Wallet.Setup();

            foreach (var item in floatVariables)
            {
                item.Setup();
                item.Reset();
            }

            foreach (var item in intVariables)
            {
                item.Setup();
                item.Reset();
            }

            Modificator.SetupAll(modificators);
        }

        void UpdateInTick()
        {
            stressVariable.AddingValue(stressPerTickVariable.Value);
        }
    }
}