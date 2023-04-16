using UnityEngine;
using TMPro;
using Game.Systems.Building;

namespace Game.Core.UI.Windows
{
    public class WindowSelectBuilding : Window
    {
        [Header("References")]
        [SerializeField] TMP_Text nameText;

        [Header("Events")]
        [SerializeField] GameEvent eventStartMoveBuilding;
        [SerializeField] GameEvent eventDestroyBuilding;

        Building selected;

        public void UpdateInformatin(Component sender, object data)
        {
            if (sender is Building building)
            {
                if (selected != building)
                    selected = building;

                UpdateInformatin();
            }
        }

        public void UpdateInformatin()
        {
            nameText.text = selected.Data.Name;
        }

        public void StartMoveBuilding()
        {
            eventStartMoveBuilding.Invoke(this, selected);
        }

        public void DestroyBuilding()
        {
            eventDestroyBuilding.Invoke(this, selected);
        }
    }
}