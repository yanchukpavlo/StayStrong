using UnityEngine;
using UnityEngine.Audio;
using Game.Utility;

namespace Game.Systems.Audio
{
    public class AudioVolumeController : MonoBehaviour
    {
        const string MasterParametr = "volumeMaster";

        [SerializeField] AudioMixer mainMixer;
        [SerializeField] ToggleVisualHelper toggleVisual;

        private void Start()
        {
            bool isOn = PlayerPrefs.GetInt(MasterParametr, 1) == 1;
            SwitchAudio(isOn);
            toggleVisual.SetToggleState(isOn);
        }

        public void SwitchAudio(bool isOn)
        {
            PlayerPrefs.SetInt(MasterParametr, isOn ? 1 : 0);
            mainMixer.SetFloat(MasterParametr, isOn ? 0 : -80);
        }
    }
}