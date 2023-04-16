using UnityEngine;
using System;

namespace Game.Core
{
    [CreateAssetMenu(menuName = "SO/Game Event", fileName = "GameEvent_", order = 1)]
    public class GameEvent : ScriptableObject
    {
        private Action<Component, object> myEvent;

        public void Invoke(Component sender, object data)
        {
            myEvent?.Invoke(sender, data);
        }

        public void RegisterListener(Action<Component, object> listener)
        {
            myEvent += listener;
        }

        public void UnregisterListener(Action<Component, object> listener)
        {
            myEvent -= listener;
        }
    }
}