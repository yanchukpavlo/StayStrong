using UnityEngine;

namespace Game.Core.UI.Windows
{
    public class WindowTutorial : Window
    {
        [SerializeField] GameObject[] pages;
        [SerializeField] GameEvent eventPauseSwitch;

        int current;

        private void OnEnable()
        {
            current = 0;
            pages[current].SetActive(true);
            eventPauseSwitch.Invoke(this, true);
        }

        public void Next()
        {
            pages[current].SetActive(false);
            ++current;
            if (current == pages.Length)
            {
                eventPauseSwitch.Invoke(this, false);
                Hide();
            }
            else
            {
                pages[current].SetActive(true);
            }
        }
    }
}