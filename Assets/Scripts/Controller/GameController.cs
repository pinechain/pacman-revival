using System.Collections;

using UnityEngine;

using PacmanRevival.Repository;
using PacmanRevival.Enumerations.Data;

namespace PacmanRevival.Controller
{
    public class GameController : MonoBehaviour
    {
        #region Repositories
        [Header("Repositories")]
        [SerializeField]
        private GameSettingsRepository gameSettings;
        [SerializeField]
        private GameDataRepository gameData;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            reset();
        }

        private void OnEnable()
        {
            gameData.subscribe(GameDataType.EatenCherries, onCherryEaten);
            gameData.subscribe(GameDataType.IsRunning, onGameStarted);
            gameData.subscribe(GameDataType.IsRunning, onGameFinished);
            gameData.subscribe(GameDataType.PacGuyIsDead, onPacGuyKilled);
        }

        private void OnDisable()
        {
            gameData.unsubscribe(GameDataType.EatenCherries, onCherryEaten);
            gameData.unsubscribe(GameDataType.IsRunning, onGameStarted);
            gameData.unsubscribe(GameDataType.IsRunning, onGameFinished);
            gameData.unsubscribe(GameDataType.PacGuyIsDead, onPacGuyKilled);
        }
        #endregion

        #region Callbacks
        private void onGameStarted()
        {
            if (gameData.IsRunning)
            {
                StartCoroutine(onTimePassedCR(1.0f));
            }
        }

        private void onGameFinished()
        {
            if (!gameData.IsRunning)
            {
                reset();
            }
        }

        private void onCherryEaten()
        {
            if (gameData.IsRunning && gameData.EatenCherries == gameData.TotalCherries)
            {
                finishGame();
            }
        }

        private void onPacGuyKilled()
        {
            if (gameData.IsRunning && gameData.PacGuyIsDead)
            {
                finishGame();
            }
        }

        private IEnumerator onTimePassedCR(float secondsPassed)
        {
            while (gameData.IsRunning)
            {
                gameData.RemainingTimeInSeconds--;
                if (gameData.RemainingTimeInSeconds == 0)
                {
                    finishGame();
                }
                yield return new WaitForSeconds(secondsPassed);
            }
        }
        #endregion

        #region Setup
        private void reset()
        {
            OnDisable();

            initCherries();
            evolveCherries();

            gameData.TotalCherries = GameObject.FindGameObjectsWithTag("Cherry").Length;
            gameData.EatenCherries = 0;
            gameData.StdHiscore = 0; // TODO: Retrieve information from storage
            gameData.RdHiscore = 0; // TODO: Retrieve information from storage
            gameData.IsRunning = false;
            gameData.CurrentScore = 0;
            gameData.RemainingTimeInSeconds = gameSettings.TotalTimeInSeconds;

            OnEnable();
        }
        private void initCherries()
        {
            foreach (GameObject pathWithDrops in GameObject.FindGameObjectsWithTag("Path with drops"))
            {
                if (!pathWithDrops.transform.GetChild(3).GetChild(1).gameObject.activeInHierarchy)
                {
                    pathWithDrops.transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
                }
            }
        }

        private void evolveCherries()
        {
            foreach (GameObject cherry in GameObject.FindGameObjectsWithTag("Cherry"))
            {
                if (Random.Range(0f, 1.0f) < gameSettings.SpecialCherrySpawnRate)
                {
                    cherry.SetActive(false);
                    cherry.transform.parent.GetChild(1).gameObject.SetActive(true);
                }
            }
        }
        #endregion

        private void finishGame() => gameData.IsRunning = false;
    }
}
