using UnityEngine;

namespace Game.Core
{
    [CreateAssetMenu(menuName = "SO/Modificator/Variable", fileName = "VM_")]
    public class VariableModificator : Modificator
    {
        [field: SerializeField] public VariableInt Amount { get; private set; }
        [field: SerializeField] public VariableFloat TargetVariable { get; private set; }
        [field: SerializeField] public VariableFloat PerTickVariable { get; private set; }

        public float Result => Amount.Value * PerTickVariable.Value;

        public override void Setup()
        {
            AdjustTargetVariable(Amount.Value, Amount.StartValue);
        }

        public override void Enable()
        {
            Amount.OnValueUpdate += OnTargetVariableChange;
            PerTickVariable.OnValueUpdate += OnPerTickVariableChange;
        }

        public override void Disable()
        {
            Amount.OnValueUpdate -= OnTargetVariableChange;
            PerTickVariable.OnValueUpdate -= OnPerTickVariableChange;
        }

        void OnTargetVariableChange(int newValue, int previousValue)
        {
            AdjustTargetVariable(newValue, previousValue);
        }

        void OnPerTickVariableChange(float newValue, float previousValue)
        {
            AdjustPerTickVariable(newValue, previousValue);
        }

        public override void ManualUpdate(int amount)
        {
            TargetVariable.AddingValue(PerTickVariable.Value * amount);
        }

        public void AdjustTargetVariable(float newValue, float previousValue)
        {
            if (newValue != previousValue)
                TargetVariable.AddingValue((newValue - previousValue) * PerTickVariable.Value);
        }

        public void AdjustPerTickVariable(float newValue, float previousValue)
        {
            if (newValue != previousValue)
                TargetVariable.AddingValue((newValue - previousValue) * Amount.Value);
        }
    }
}