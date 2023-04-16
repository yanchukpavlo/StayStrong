using UnityEngine;
using UnityEngine.Events;

namespace Game.Core.Event
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent gameEvent;
        public GameEventCustom response;

        #region Unity


        private void OnEnable()
        {
            gameEvent.RegisterListener(OnEventInvoked);
        }

        private void OnDisable()
        {
            gameEvent.UnregisterListener(OnEventInvoked);
        }

        #endregion

        public void OnEventInvoked(Component sender, object data)
        {
            response.Invoke(sender, data);
        }
    }

    [System.Serializable]
    public class GameEventCustom : UnityEvent<Component, object> { }
}