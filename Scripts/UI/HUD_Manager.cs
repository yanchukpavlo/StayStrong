using UnityEngine;
using Game.Utility;
using Game.Core.UI.Windows;

namespace Game.Core.UI
{
    public class HUD_Manager : StaticInstance<HUD_Manager>
    {
        public void WindowEnable(Window window)
        {
            window.Show();
        }

        public void GameStartNew()
        {
            GameCore.Instance.GameNew();
        }

        public void GameContinue()
        {
            GameCore.Instance.GameContinue();
        }

        public void GoToMenu()
        {
            SceneLoader.Instance.LoadScene(Scenes.Menu);
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void URL_SaveLife()
        {
            Application.OpenURL("https://savelife.in.ua/en/");
        }
    }
}