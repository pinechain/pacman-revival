using System;

using UnityEngine;

using PacmanRevival.Enumerations.Data;

namespace PacmanRevival.Repository
{
    [CreateAssetMenu(fileName = "Game data", menuName = "Pacman Revival/Game data", order = 1)]
    public class GameDataRepository : ScriptableObject
    {
        #region Properties
        public int TotalCherries
        {
            get => totalCherries;
            set => trigger(GameDataType.TotalCherries, value);
        }
        public int EatenCherries
        {
            get => eatenCherries;
            set => trigger(GameDataType.EatenCherries, value);
        }
        public bool SpecialCherryIsConsumed
        {
            get => specialCherryIsConsumed;
            set => trigger(GameDataType.SpecialCherryIsConsumed, value);
        }
        public int StdHiscore
        {
            get => standardHiscore;
            set => trigger(GameDataType.StdHiscore, value);
        }
        public int RdHiscore
        {
            get => randomHiscore;
            set => trigger(GameDataType.RdHiscore, value);
        }
        public int RemainingTimeInSeconds
        {
            get => remainingTimeInSeconds;
            set => trigger(GameDataType.RemainingTimeInSeconds, value);
        }
        public int CurrentScore
        {
            get => currentScore;
            set => trigger(GameDataType.CurrentScore, value);
        }
        public bool IsRunning
        {
            get => isRunning;
            set => trigger(GameDataType.IsRunning, value);
        }
        public bool PacGuyIsDead
        {
            get => pacGuyIsDead;
            set => trigger(GameDataType.PacGuyIsDead, value);
        }
        #endregion

        #region Backing Fields
        [Header("Cherries")]
        [SerializeField]
        private int totalCherries;
        [SerializeField]
        private int eatenCherries;
        [SerializeField]
        private bool specialCherryIsConsumed;

        [Header("Score")]
        [SerializeField]
        private int standardHiscore;
        [SerializeField]
        private int randomHiscore;
        [SerializeField]
        private int currentScore;

        [Header("Time")]
        [SerializeField]
        private int remainingTimeInSeconds;

        [Header("Flags")]
        [SerializeField]
        private bool isRunning;
        [SerializeField]
        private bool pacGuyIsDead;
        #endregion

        #region Notifications
        public delegate void OnDataChanged();
        private OnDataChanged[] onDataChanged = new OnDataChanged[Enum.GetValues(typeof(GameDataType)).Length];
        public void subscribe(GameDataType gameDataType, OnDataChanged func) => onDataChanged[(int)gameDataType] += func;
        public void unsubscribe(GameDataType gameDataType, OnDataChanged func) => onDataChanged[(int)gameDataType] -= func;
        private void trigger(GameDataType gameDataType, int value)
        {
            switch (gameDataType)
            {
                case GameDataType.TotalCherries:
                    totalCherries = value;
                    break;
                case GameDataType.EatenCherries:
                    eatenCherries = value;
                    break;
                case GameDataType.StdHiscore:
                    standardHiscore = value;
                    break;
                case GameDataType.RdHiscore:
                    randomHiscore = value;
                    break;
                case GameDataType.CurrentScore:
                    currentScore = value;
                    break;
                case GameDataType.RemainingTimeInSeconds:
                    remainingTimeInSeconds = value;
                    break;
            }

            notifyChange(gameDataType);
        }
        private void trigger(GameDataType gameDataType, bool value)
        {
            switch (gameDataType)
            {
                case GameDataType.IsRunning:
                    isRunning = value;
                    break;
                case GameDataType.SpecialCherryIsConsumed:
                    specialCherryIsConsumed = value;
                    break;
                case GameDataType.PacGuyIsDead:
                    pacGuyIsDead = value;
                    break;
            }

            notifyChange(gameDataType);
        }

        private void notifyChange(GameDataType gameDataType)
        {
            if (onDataChanged[(int)gameDataType] != null)
            {
                onDataChanged[(int)gameDataType]();
            }
        }
        #endregion
    }
}
