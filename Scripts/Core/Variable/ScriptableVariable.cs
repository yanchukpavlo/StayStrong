using UnityEngine;
using Game.Systems.Save;
using System;

namespace Game.Core
{
    public abstract class ScriptableVariable<T> : ScriptableObject, ISaveable, IVariable
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public T Value { get; private set; }
        [field: SerializeField, Space] public T StartValue { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Prefix { get; private set; }

        public string ValueString => Value.ToString();

        public event MyAction OnValueUpdate;
        public delegate void MyAction(T newValue, T previousValue);

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(Id))
                Id = ISaveable.GetId();
        }

        public abstract void AddingValue(T add);

        public virtual void Setup()
        {
            ISaveable.Saveables[Id] = this;
        }

        public virtual void UpdateValue(T newValue)
        {
            if (Value.Equals(newValue))
                return;

            OnValueUpdate?.Invoke(newValue, Value);
            Value = newValue;
        }

        public void Reset()
        {
            UpdateValue(StartValue);
        }

        public object SaveState()
        {
            return Value;
        }

        public void LoadState(object data)
        {
            UpdateValue(CastObjectTo(data));
        }

        public virtual T CastObjectTo(object readData)
        {
            if (readData is T)
            {
                return (T)readData;
            }
            try
            {
                return (T)Convert.ChangeType(readData, typeof(T));
            }
            catch (InvalidCastException)
            {
                return default(T);
            }
        }
    }
}