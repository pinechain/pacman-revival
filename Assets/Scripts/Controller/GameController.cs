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
            gameData.TotalCherries = GameObject.FindGameObjectsWithTag("Cherry").Length;
            gameData.EatenCherries = 0;
            gameData.StdHiscore = 0; // TODO: Retrieve information from storage
            gameData.RdHiscore = 0; // TODO: Retrieve information from storage
            gameData.IsRunning = false;
            gameData.CurrentScore = 0;
            gameData.RemainingTimeInSeconds = gameSettings.TotalTimeInSeconds;

            initCherries();
        }

        private void OnEnable()
        {
            gameData.subscribe(GameDataType.EatenCherries, onCherryEaten);
            gameData.subscribe(GameDataType.IsRunning, onGameStarted);
            gameData.subscribe(GameDataType.PacGuyIsDead, onPacGuyKilled);
        }

        private void OnDisable()
        {
            gameData.unsubscribe(GameDataType.EatenCherries, onCherryEaten);
            gameData.unsubscribe(GameDataType.IsRunning, onGameStarted);
            gameData.unsubscribe(GameDataType.PacGuyIsDead, onPacGuyKilled);
        }
        #endregion

        #region Callbacks
        private void onGameStarted() => StartCoroutine(onTimePassedCR(1.0f));

        private void onGameFinished(bool victory)
        {
            gameData.IsRunning = false;
            // TODO: Calculate final score
            // TODO: Calculate hiscore
            if (victory)
            {
                onGameWon();
            }
            else
            {
                onGameLost();
            }
        }

        private void onGameWon()
        {
            // TODO: Show victory screen
            Debug.Log("You win!");
        }

        private void onGameLost()
        {
            // TODO: Show Losing screen
            Debug.Log("You lose!");
        }

        private void onCherryEaten()
        {
            if (gameData.IsRunning && gameData.EatenCherries == gameData.TotalCherries)
            {
                onGameFinished(true);
            }
        }

        private void onPacGuyKilled()
        {
            if (gameData.IsRunning && gameData.PacGuyIsDead)
            {
                onGameFinished(false);
            }
        }

        private IEnumerator onTimePassedCR(float secondsPassed)
        {
            while (gameData.IsRunning)
            {
                gameData.RemainingTimeInSeconds--;
                if (gameData.RemainingTimeInSeconds == 0)
                {
                    onGameFinished(false);
                }
                yield return new WaitForSeconds(secondsPassed);
            }
        }
        #endregion

        #region Setup
        private void initCherries()
        {
            foreach (GameObject cherry in GameObject.FindGameObjectsWithTag("Cherry"))
            {
                if (Random.Range(0f, 1.0f) < gameSettings.SpecialCherrySpawnRate)
                {
                    evolveCherry(cherry);
                }
            }
        }

        private void evolveCherry(GameObject cherry)
        {
            cherry.SetActive(false);
            cherry.transform.parent.GetChild(1).gameObject.SetActive(true);
        }
        #endregion
    }
}
