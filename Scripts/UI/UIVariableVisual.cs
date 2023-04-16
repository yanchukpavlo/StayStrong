using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.Core.UI
{
    public class UIVariableVisual : MonoBehaviour
    {
        [SerializeField] Image imageIcon;
        [SerializeField] TextMeshProUGUI textName;
        [SerializeField] TextMeshProUGUI textValue;
        [SerializeField] TextMeshProUGUI textPrefix;

        IVariable variable;

        private void OnEnable()
        {
            if (variable != null)
                UpdateInfo(variable, variable.ValueString);
        }

        public void Setup(IVariable targetVariable)
        {
            variable = targetVariable;

            if (string.IsNullOrEmpty(targetVariable.Prefix))
                textPrefix.gameObject.SetActive(false);

            UpdateInfo(variable, variable.ValueString);
        }

        public void Setup(IVariable variable, string value)
        {            
            if (string.IsNullOrEmpty(variable.Prefix))
                textPrefix.gameObject.SetActive(false);

            UpdateInfo(variable, value);
        }

        void UpdateInfo(IVariable variable, string value)
        {
            imageIcon.sprite = variable.Icon;
            textName.text = variable.Name;
            textValue.text = value;
            textPrefix.text = variable.Prefix;
        }
    }
}