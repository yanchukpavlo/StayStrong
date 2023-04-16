using System;
using System.Collections;
using UnityEngine;
using Game.Utility;
using Game.Systems.Save;
using Game.Core.UI;

namespace Game.Core
{
    public enum GameState
    {
        Wait,
        Pause,
        Play
    }

    public class GameCore : PersistentSingletone<GameCore>
    {

        public static float DeltaTime { get; private set; }
        public static GameState State { get; private set; } = GameState.Wait;

        [SerializeField] bool test = false;
        [SerializeField] float TickTime = 1f;
        [SerializeField] VariableFloat stressVariable;

        [Header("Events")]
        [SerializeField] GameEvent eventOpenTutorial;
        [SerializeField] GameEvent eventGameWin;
        [SerializeField] GameEvent eventGameOver;
        [SerializeField] GameEvent eventPauseWindowSwitch;

        float tickTimer;

        private void OnEnable()
        {
            stressVariable.OnMaxValueReached += OnGameOver;
            eventPauseWindowSwitch.RegisterListener(OnPauseWindowSwitch);
            eventGameWin.RegisterListener(OnGameWin);
        }

        private void OnDisable()
        {
            stressVariable.OnMaxValueReached -= OnGameOver;
            eventPauseWindowSwitch.UnregisterListener(OnPauseWindowSwitch);
            eventGameWin.UnregisterListener(OnGameWin);
        }

        private void Start()
        {
            if (test)
            {
                State = GameState.Play;
                SceneLoader.Instance.Off();
            }
            else
            {
                SceneLoader.Instance.LoadScene(Scenes.Menu);
            }
        }

        void Update()
        {
            if (State != GameState.Play)
                return;

            DeltaTime = Time.deltaTime;
            EventsManager.InvokeUpdate();


            if ((tickTimer += DeltaTime) > TickTime)
            {
                EventsManager.InvokeTick();
                tickTimer -= TickTime;
            }
        }

        void OnGameOver()
        {
            State = GameState.Wait;
            eventGameOver.Invoke(this, null);
            Debug.Log("Gameover :(");
            StartCoroutine(Helper.WaitAdnDo(5f, () => HUD_Manager.Instance.GoToMenu()));
        }

        void OnGameWin(Component sender, object data)
        {
            State = GameState.Wait;
            Debug.Log("Win :)");
            StartCoroutine(Helper.WaitAdnDo(8f, () => HUD_Manager.Instance.GoToMenu()));
        }

        void OnPauseWindowSwitch(Component sender, object data)
        {
            if ((bool)data)
                State = GameState.Pause;
            else
                State = GameState.Play;
        }

        public void GameContinue()
        {
            DataManager.Instance.ResetVariables();
            SceneLoader.Instance.LoadOperation.completed += OnGameContinueLoad;
            SceneLoader.Instance.LoadScene(Scenes.Game, false);
        }

        void OnGameContinueLoad(AsyncOperation operation)
        {
            SceneLoader.Instance.LoadOperation.completed -= OnGameContinueLoad;
            
            StartCoroutine(Helper.WaitAdnDo(1f, () =>
                {
                    SaveLoadSystem.Instance.Load();
                    SceneLoader.Instance.Off();
                    State = GameState.Play;
                    Debug.Log("Continue");
                }
            ));
        }

        public void GameNew()
        {
            DataManager.Instance.ResetVariables();
            SceneLoader.Instance.LoadOperation.completed += OnGameNewLoad;
            SceneLoader.Instance.LoadScene(Scenes.Game, false);
        }

        void OnGameNewLoad(AsyncOperation operation)
        {
            StartCoroutine(Helper.WaitAdnDo(1f, () =>
                {
                    SceneLoader.Instance.LoadOperation.completed -= OnGameNewLoad;
                    eventOpenTutorial.Invoke(this, null);
                    SceneLoader.Instance.Off();
                }
            ));

        }
    }
}