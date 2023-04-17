using Game.Utility;
using UnityEngine;

namespace Game.Core.UI.Windows
{
    public class WindowPause : Window
    {
        [SerializeField] CanvasGroup canvasGroup;
        [Header("Events")]
        [SerializeField] GameEvent eventPauseWindowSwitch;

        public override void Show()
        {
            eventPauseWindowSwitch.Invoke(this, true);
            canvasGroup.interactable = true;
            base.Show();
        }

        public override void Hide()
        {
            eventPauseWindowSwitch.Invoke(this, false);
            base.Hide();
        }

        public void ExitToMenu()
        {
            canvasGroup.interactable = false;
            Save();

            StartCoroutine(Helper.WaitAdnDo(1f, () =>
                {
                    canvasGroup.interactable = true;
                    HUD_Manager.Instance.GoToMenu();
                }
            ));
        }

        public void Exit()
        {
            canvasGroup.interactable = false;
            Save();

            StartCoroutine(Helper.WaitAdnDo(1f, () =>
                {
                    canvasGroup.interactable = true;
                    HUD_Manager.Instance.Exit();
                }
            ));
        }

        public void Save()
        {
            Systems.Save.SaveLoadSystem.Instance.Save();
        }
    }
}