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
            Save();

            StartCoroutine(Helper.WaitAdnDo(1f, () =>
                {
                    canvasGroup.interactable = true;
                    HUD_Manager.Instance.GoToMenu();
                    Debug.Log("ExitToMenu");
                }
            ));
        }

        public void Exit()
        {
            Save();

            StartCoroutine(Helper.WaitAdnDo(1f, () =>
                {
                    canvasGroup.interactable = true;
                    HUD_Manager.Instance.Exit();
                    Debug.Log("Exit");
                }
            ));
        }

        public void Save()
        {
            Systems.Save.SaveLoadSystem.Instance.Save();
            Debug.Log("Save");
        }
    }
}