using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Systems.Occasion;
using Game.Utility;

namespace Game.Core.UI
{
    public class UIOccasion : MonoBehaviour
    {
        [SerializeField] Transform conditionRoot;
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI textName;
        [SerializeField] TextMeshProUGUI textDescription;
        [SerializeField] Slider timerSlider;

        Occasion currentOccasion;

        public void OnNewOccasion(Component sender, object data)
        {
            Setup((Occasion)data);
        }

        public void ButtonAccept() => currentOccasion.Accept();
        public void ButtonDeny() => currentOccasion.Deny();

        void Setup(Occasion occasion)
        {
            currentOccasion = occasion;

            icon.sprite = occasion.Icon;
            textName.text = occasion.Name;
            textDescription.text = occasion.GetDescription();
            timerSlider.maxValue = occasion.Timer;
            timerSlider.value = occasion.Timer;

            occasion.GetCondition(conditionRoot);

            occasion.OnDone += Close;
            occasion.OnFail += Close;
            occasion.OnTimerUpdate += UpdateTimer;

            gameObject.SetActive(true);
        }

        void Close()
        {
            currentOccasion.OnDone -= Close;
            currentOccasion.OnFail -= Close;
            currentOccasion.OnTimerUpdate -= UpdateTimer;

            currentOccasion = null;

            gameObject.SetActive(false);
            conditionRoot.DeleteChildren();
        }

        void UpdateTimer(int remainder)
        {
            timerSlider.value = remainder;
        }
    }
}