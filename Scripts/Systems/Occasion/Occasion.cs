using System;
using UnityEngine;
using Game.Core;

namespace Game.Systems.Occasion
{
    public abstract class Occasion : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; } = "Occasion Name";
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField, Min(0)] public int Timer { get; private set; } = 0;

        [SerializeField, TextArea] protected string description = "Occasion Description";

        public event Action OnDone;
        public event Action OnFail;
        public event Action<int> OnTimerUpdate;

        int timeToFail;

        public abstract void GetCondition(Transform conditionRoot);
        public abstract string GetDescription();
        protected abstract bool IsCanBeAccepted();

        public virtual void Accept()
        {
            if (IsCanBeAccepted())
                Done();
        }

        public virtual void Deny()
        {
            Fail();
        }

        public virtual void Setup()
        {
            if (Timer != 0)
            {
                timeToFail = Timer;
                EventsManager.OnTick += OnUpdateTimer;
            }
        }

        protected virtual void OnUpdateTimer()
        {
            if (--timeToFail == 0)
                Fail();

            OnTimerUpdate?.Invoke(timeToFail);
        }

        protected virtual void Done()
        {
            if (Timer != 0)
                EventsManager.OnTick -= OnUpdateTimer;

            OnDone?.Invoke();
        }

        protected virtual void Fail()
        {
            OnFail?.Invoke();
        }
    }
}