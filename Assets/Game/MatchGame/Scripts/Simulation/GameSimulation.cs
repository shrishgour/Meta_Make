using System;
using System.Collections;
using POPBlocks.Scripts.LevelHandle;
using POPBlocks.Scripts.Utils;
//using Unity.Simulation.Games;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace POPBlocks.Scripts.Simulation
{
    public class GameSimulation : MonoBehaviour
    {
        public bool enable;
        public int playsMax = 10;
        public int played;
        [SerializeField] private float timeScale = 10;
        private int level;
        private int colors;
        private int moves;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            Debug.Log(Application.persistentDataPath);
        }

        private void OnEnable()
        {
            #if SIMULATION_ENABLED
            LevelManager.OnLevelLoaded += OnLevelLoaded;
            LevelManager.WinLoseEvent += SetCounter;
            SceneManager.sceneLoaded += OnSceneLoaded;
            StartGame();
            #endif
        }

        private void OnDisable()
        {
            #if SIMULATION_ENABLED
            LevelManager.OnLevelLoaded -= OnLevelLoaded;
            LevelManager.WinLoseEvent -= SetCounter;
            SceneManager.sceneLoaded -= OnSceneLoaded;
            #endif
        }

        private void OnValidate()
        {
            if (Application.isPlaying) return;
            #if UNITY_EDITOR
            if (enable)
            {
                DefineSymbolsUtils.AddSymbol("SIMULATION_ENABLED");
            }
            else
            {
                DefineSymbolsUtils.DeleteSymbol("SIMULATION_ENABLED");
            }
            #endif
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
        {
            if(scene.name == "game")
                StartCoroutine(WaitForGame());
        }

        private void OnLevelLoaded()
        {
            Field.Instance.AI = true;
         }

        private void LateUpdate()
        {
#if SIMULATION_ENABLED
            Time.timeScale = timeScale;
#endif
        }

        private void StartGame()
        {
            //GameSimManager.Instance.FetchConfig(OnConfigFetched);

        }
        //void OnConfigFetched(GameSimConfigResponse response)
        //{
        //    Debug.Log("Got a config!");
        //    level = response.GetInt("level");
        //    colors = response.GetInt("colors");
        //    moves = response.GetInt("moves");
        //    Debug.Log("Simulation started: level " + level);
        //    PlayerPrefs.SetInt("OpenLevelTest", level);
        //    SceneManager.LoadScene("game");
        //}

        private IEnumerator WaitForGame()
        {
            yield return new WaitWhile(() => LevelManager.Instance == null);
            LevelManager.Instance.LoadLevel(level, colors, moves);
        }

        private void SetCounter(int score, int stars, int movesRest, bool fail)
        {
            //GameSimManager.Instance.SetCounter("score", score);
            //GameSimManager.Instance.SetCounter("stars", stars);
            //GameSimManager.Instance.SetCounter("movesRest", movesRest);
            //GameSimManager.Instance.SetCounter("win", fail?0:1);
            //played++;
            //if(played >= playsMax) Quit();
            //else
            //    LevelManager.Instance.RestartLevel();

        }

        

        void Quit()
        {
#if SIMULATION_ENABLED
            Debug.Log("Simulation completed" );
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            Debug.Log("Quit running");
#endif
        }
    }
}
