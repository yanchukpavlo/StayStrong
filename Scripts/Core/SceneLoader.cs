using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Utility
{
    public enum Scenes
    {
        Init,
        Menu,
        Game
    }

    public class SceneLoader : PersistentSingletone<SceneLoader>
    {
        public AsyncOperation LoadOperation { get; private set; }

        [SerializeField] GameObject visual;

        public void LoadScene(Scenes scene, bool offAfter = true)
        {
            visual.SetActive(true);
            LoadOperation = SceneManager.LoadSceneAsync((int)scene);
            if (offAfter)
                LoadOperation.completed += LoadCompleted;
        }

        public void Off() => visual.SetActive(false);

        void LoadCompleted(AsyncOperation operation)
        {
            operation.completed -= LoadCompleted;
            visual.SetActive(false);
        }
    }
}