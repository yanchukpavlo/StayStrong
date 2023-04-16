using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Systems.Occasion;
using Game.Utility;

namespace Game.Core.UI
{
    public class UIOccasion : MonoBehaviour
    {
        [SerializeField] GameObject visualRoot;
        [SerializeField] Transform conditionRoot;
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI textName;
        [SerializeField] TextMeshProUGUI textDescription;
        [SerializeField] Slider timerSlider;

        [Header("Events")]
        [SerializeField] GameEvent eventNewOccasion;

        Occasion currentOccasion;

        private void OnEnable()
        {
            eventNewOccasion.RegisterListener(OnNewOccasion);
        }

        private void OnDisable()
        {
            eventNewOccasion.UnregisterListener(OnNewOccasion);
        }

        public void ButtonAccept() => currentOccasion.Accept();
        public void ButtonDeny() => currentOccasion.Deny();

        void OnNewOccasion(Component sender, object data)
        {
            Setup((Occasion)data);
        }

        void Setup(Occasion occasion)
        {
            currentOccasion = occasion;

            icon.sprite = occasion.Icon;
            textName.text = occasion.Name;
            textDescription.text = occasion.GetDescription();
            timerSlider.maxValue = occasion.Timer;
            timerSlider.value = occasion.Timer;

            occasion.GetCondition(conditionRoot);

            visualRoot.SetActive(true);

            occasion.OnDone += Close;
            occasion.OnFail += Close;
            occasion.OnTimerUpdate += UpdateTimer;
        }

        void Close()
        {
            currentOccasion.OnDone -= Close;
            currentOccasion.OnFail -= Close;
            currentOccasion.OnTimerUpdate -= UpdateTimer;

            currentOccasion = null;

            visualRoot.SetActive(false);
            conditionRoot.DeleteChildren();
        }

        void UpdateTimer(int remainder)
        {
            timerSlider.value = remainder;
        }
    }
}