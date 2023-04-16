using System;
using UnityEngine;

namespace Game.Core
{
    [CreateAssetMenu(menuName = "SO/Variable/Int", fileName = "VariableInt_")]
    public class VariableInt : ScriptableVariable<int>
    {
        [field: SerializeField, Space] public bool IsInvokeMinReached { get; private set; } = false;
        [field: SerializeField] public bool IsInvokeMaxReached { get; private set; } = false;
        [field: SerializeField] public Vector2Int Range { get; private set; }

        public event Action OnMinValueReached;
        public event Action OnMaxValueReached;

        public int MinValue => Range.x;
        public int MaxValue => Range.y;
        public float Percent => (float)Value / (float)Range.y;

        public override void AddingValue(int add)
        {
            UpdateValue(Value + add);
        }

        public override void UpdateValue(int newValue)
        {
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
            AddingValue(1);
        }

        [ContextMenu("SubtractOne")]
        void SubtractOne()
        {
            AddingValue(-1);
        }
    }
}