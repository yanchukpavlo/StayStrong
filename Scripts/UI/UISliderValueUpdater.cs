using UnityEngine;
using UnityEngine.UI;

namespace Game.Core.UI
{
    [RequireComponent(typeof(Slider))]
    public class UISliderValueUpdater : MonoBehaviour
    {
        [SerializeField] VariableFloat variableFloat;
        [SerializeField] Slider slider;

        VariableFloat variable;

        private void OnValidate()
        {
            if (!slider)
                slider = GetComponent<Slider>();
        }

        private void Awake()
        {
            if (variableFloat)
                Setup(variableFloat);
        }

        public void Setup(VariableFloat newVariable)
        {
            if (variable)
                variable.OnValueUpdate -= UpdateSlider;

            slider.maxValue = newVariable.MaxValue;
            slider.value = newVariable.Value;
            variable = newVariable;
        }

        private void OnEnable()
        {
            variable.OnValueUpdate += UpdateSlider;
        }

        private void OnDisable()
        {
            variable.OnValueUpdate -= UpdateSlider;
        }

        void UpdateSlider(float newValue, float previousValue)
        {
            slider.value = newValue;
        }
    }
}