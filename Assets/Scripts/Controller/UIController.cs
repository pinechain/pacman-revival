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
        private const string PREFIX_HISCORE = "Hiscore: ";
        private const string PREFIX_SCORE = "Score: ";
        private const string PREFIX_TIME_LEFT = "Time left: ";
        private const string PREFIX_FRUITS_LEFT = "Fruits left: ";
        private const string ANNOUNCEMENT_VICTORY = "You win!";
        private const string ANNOUNCEMENT_FAILURE = "You lose!";
        private const string ANNOUNCEMENT_TODO_TITLE = "Feature not yet created!";
        private const string ANNOUNCEMENT_TODO_MESSAGE = "Coming soon...";
        
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

        [SerializeField]
        private TMP_Text announcementTitle;

        [SerializeField]
        private TMP_Text announcementMessage;
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
            gameData.subscribe(GameDataType.IsRunning, onGameFinished);
        }

        private void OnDisable()
        {
            gameData.unsubscribe(GameDataType.EatenCherries, onCherryEaten);
            gameData.unsubscribe(GameDataType.RemainingTimeInSeconds, onTimePassed);
            gameData.unsubscribe(GameDataType.CurrentScore, onScoreChanged);
            gameData.unsubscribe(GameDataType.IsRunning, onGameFinished);
        }
        #endregion

        #region Callbacks
        public void onCherryEaten() => fruitsLeft.text = PREFIX_FRUITS_LEFT + (gameData.TotalCherries - gameData.EatenCherries);
        public void onTimePassed() => timeLeft.text = PREFIX_TIME_LEFT + gameData.RemainingTimeInSeconds;
        public void onScoreChanged() => score.text = PREFIX_SCORE + gameData.CurrentScore;
        public void onGameFinished()
        {
            if (!gameData.IsRunning)
            {
                showAnnouncement(gameData.PacGuyIsDead || gameData.RemainingTimeInSeconds <= 0 ? ANNOUNCEMENT_FAILURE : ANNOUNCEMENT_VICTORY, PREFIX_SCORE + gameData.CurrentScore);
            }
        }
        #endregion

        #region Buttons
        public void startStandardGame() => gameData.IsRunning = true;
        public void startRandomGame() => showAnnouncement(ANNOUNCEMENT_TODO_TITLE, ANNOUNCEMENT_TODO_MESSAGE);
        #endregion

        public void showAnnouncement(string title, string message)
        {
            announcementMessage.transform.parent.gameObject.SetActive(true);
            announcementTitle.text = title;
            announcementMessage.text = message;
        }

        public void hideAnnouncement() => announcementMessage.transform.parent.gameObject.SetActive(false);
    }
}
