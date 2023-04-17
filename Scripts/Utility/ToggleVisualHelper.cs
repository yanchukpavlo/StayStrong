using UnityEngine;
using UnityEngine.UI;

namespace Game.Utility
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleVisualHelper : MonoBehaviour
    {
        [SerializeField] Sprite toggleOnIcon;
        [SerializeField] Sprite toggleOffIcon;
        [Space]
        [SerializeField] Image icon;

        Toggle _toggle;
        Toggle Toggle
        {
            get
            {
                if (!_toggle)
                    _toggle = GetComponent<Toggle>();

                return _toggle;
            }
        }

        private void Awake()
        {
            Toggle.onValueChanged.AddListener(SwitchToggle);
        }

        public void SetToggleState(bool isOn)
        {
            Toggle.isOn = isOn;
            SwitchToggle(isOn);
        }

        void SwitchToggle(bool isOn)
        {
            icon.sprite = isOn ? toggleOnIcon : toggleOffIcon;
        }
    }
}