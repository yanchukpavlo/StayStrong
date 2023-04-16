using UnityEngine;
using Game.Core;

namespace Game.Systems.Occasion
{
    public class OccasionManager : MonoBehaviour
    {
        [SerializeField] Occasion[] occasions;
        [SerializeField] Vector2Int occasionDelay = new Vector2Int(100, 200);

        [Header("Events")]
        [SerializeField] GameEvent eventNewOccasion;

        int timer;

        public void OnEnable()
        {
            timer = occasionDelay.RandomRange();
            EventsManager.OnTick += UpdateTimerToOccasion;
        }

        public void OnDisable()
        {
            EventsManager.OnTick -= UpdateTimerToOccasion;
        }

        void UpdateTimerToOccasion()
        {
            if (--timer == 0)
            {
                GetOccasions(out Occasion occasion);
                timer = occasion.Timer + occasionDelay.RandomRange();
                eventNewOccasion.Invoke(this, occasion);
                occasion.Setup();
            }
        }

        void GetOccasions(out Occasion occasion)
        {
            occasion = occasions[Random.Range(0, occasions.Length)];
        }
    }
}