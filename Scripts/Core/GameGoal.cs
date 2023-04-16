using UnityEngine;

namespace Game.Core
{
    public class GameGoal : MonoBehaviour
    {
        [SerializeField] GameEvent eventWinGame;

        private void OnEnable()
        {
            eventWinGame?.Invoke(this, null);
        }
    }
}