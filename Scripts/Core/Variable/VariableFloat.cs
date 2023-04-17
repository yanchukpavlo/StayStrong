using System;
using UnityEngine;

namespace Game.Core
{
    [CreateAssetMenu(menuName = "SO/Variable/Float", fileName = "VariableFloat_")]
    public class VariableFloat : ScriptableVariable<float>
    {
        [field: SerializeField, Space] public bool IsInvokeMinReached { get; private set; } = false;
        [field: SerializeField] public bool IsInvokeMaxReached { get; private set; } = false;
        [field: SerializeField] public Vector2Int Range { get; private set; }
        public event Action OnMinValueReached;
        public event Action OnMaxValueReached;

        public float MinValue => Range.x;
        public float MaxValue => Range.y;
        public float Percent => Value / Range.y;

        public override void AddingValue(float add) => UpdateValue(Value + add);

        public override void UpdateValue(float newValue)
        {
            newValue = (float)Math.Round((double)newValue, 3);

            if (Range != Vector2Int.zero)
            {
                if (newValue <= Range.x)
                {
                    newValue = Range.x;
                    if (IsInvokeMinReached)
                        OnMinValueReached?.Invoke();
                }
                if (newValue >= Range.y)
                {
                    newValue = Range.y;
                    if (IsInvokeMaxReached)
                        OnMaxValueReached?.Invoke();
                }
            }

            base.UpdateValue(newValue);
        }

        [ContextMenu("AddOne")]
        void AddOne()
        {
            AddingValue(1f);
        }

        [ContextMenu("SubtractOne")]
        void SubtractOne()
        {
            AddingValue(-1f);
        }
    }
}