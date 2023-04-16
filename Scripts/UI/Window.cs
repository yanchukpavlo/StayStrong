using UnityEngine;

namespace Game.Core.UI.Windows
{
    public class Window : MonoBehaviour
    {
        static Window currentActive;

        public virtual void Show()
        {
            if (currentActive)
                currentActive.Hide();

            currentActive = this;
            currentActive.gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            currentActive.gameObject.SetActive(false);
            currentActive = null;
        }
    }
}