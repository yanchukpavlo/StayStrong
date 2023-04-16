using UnityEngine;
using UnityEngine.UI;
using Game.Systems.Save;

namespace Game.Core.UI.Windows
{
    public class WindowMain : Window
    {
        [SerializeField] Button buttonContinue;

        private void Start()
        {
            buttonContinue.interactable = SaveLoadSystem.Instance.CheckSaveFile();
        }
    }
}