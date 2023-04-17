using UnityEngine;

namespace Game.Systems.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSFXHandler : MonoBehaviour
    {
        [SerializeField] AudioClip[] clips;
        [SerializeField] AudioSource source;
        [SerializeField] bool playOnEnable = true;

        [Header("Settings")]
        [SerializeField, Min(0)] float delay;

        private void OnValidate()
        {
            if (!source)
                source = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            if (playOnEnable)
                PlayRandom();
        }

        public void PlayRandom()
        {
            source.clip = clips[Random.Range(0, clips.Length)];

            if (delay.IsZero())
                source.Play();
            else
                source.PlayDelayed(delay);
        }
    }
}