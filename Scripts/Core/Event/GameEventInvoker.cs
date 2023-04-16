using UnityEngine;
using System.Collections.Generic;

namespace Game.Core.Event
{
    public class GameEventInvoker : MonoBehaviour
    {
        [SerializeField] List<GameEvent> events = new List<GameEvent>();

        public void InvokeEvents()
        {
            if (events.Count == 0)
            {
                Debug.LogWarning($"Events list on {name} - empty.");
                return;
            }

            foreach (var item in events)
                item.Invoke(null, null);
        }
    }
}