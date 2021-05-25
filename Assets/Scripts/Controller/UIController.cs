using TMPro;
using System;

using UnityEngine;

using PacmanRevival.Repository;
using PacmanRevival.Enumerations.Data;

namespace PacmanRevival.Controller
{
    public class UIController : MonoBehaviour
    {
        #region Text Constants
        [Header("Text constants")]
        [SerializeField]
        private const string PREFIX_HISCORE = "Hiscore: ";
        [SerializeField]
        private const string PREFIX_SCORE = "Score: ";
        [SerializeField]
        private const string PREFIX_TIME_LEFT = "Time left: ";
        [SerializeField]
        private const string PREFIX_FRUITS_LEFT = "Fruits left: ";
        #endregion

        #region Labels
        [Header("Labels")]
        [SerializeField]
        private TMP_Text rdHiscore;

        [SerializeField]
        private TMP_Text stdHiscore;

        [SerializeField]
        private TMP_Text timeLeft;

        [SerializeField]
        private TMP_Text score;

        [SerializeField]
        private TMP_Text fruitsLeft;
        #endregion

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
            rdHiscore.text = PREFIX_HISCORE + gameData.RdHiscore;
            stdHiscore.text = PREFIX_HISCORE + gameData.StdHiscore;

            onScoreChanged();
            onTimePassed();
            onCherryEaten();
        }

        private void OnEnable()
        {
            gameData.subscribe(GameDataType.EatenCherries, onCherryEaten);
            gameData.subscribe(GameDataType.RemainingTimeInSeconds, onTimePassed);
            gameData.subscribe(GameDataType.CurrentScore, onScoreChanged);
        }

        private void OnDisable()
        {
            gameData.unsubscribe(GameDataType.EatenCherries, onCherryEaten);
            gameData.unsubscribe(GameDataType.RemainingTimeInSeconds, onTimePassed);
            gameData.unsubscribe(GameDataType.CurrentScore, onScoreChanged);
        }
        #endregion

        #region Callbacks
        public void onCherryEaten() => fruitsLeft.text = PREFIX_FRUITS_LEFT + (gameData.TotalCherries - gameData.EatenCherries);
        public void onTimePassed() => timeLeft.text = PREFIX_TIME_LEFT + gameData.RemainingTimeInSeconds;
        public void onScoreChanged() => score.text = PREFIX_SCORE + gameData.CurrentScore;
        public void startGame() => gameData.IsRunning = true;
        #endregion
    }
}
