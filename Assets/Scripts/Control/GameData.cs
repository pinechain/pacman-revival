using System;

using UnityEngine;

namespace PacmanRevival.Control
{
    [CreateAssetMenu(fileName="Game Data", menuName="Pacman Revival/Game Data", order=1)]
    public class GameData : ScriptableObject
    {
        #region Properties
        public int TotalCherries
        {
            get => totalCherries;
        }
        public int TotalTime
        {
            get => totalTime;
        }
        public int Hiscore
        {
            get => hiscore;
        }
        public int EatenCherries
        {
            get => eatenCherries;
        }
        public int TimeLeft
        {
            get => timeLeft;
        }
        public int CurrentScore
        {
            get => currentScore;
        }
        #endregion

        #region Members
        [SerializeField]
        private int totalCherries;
        [SerializeField]
        private int totalTime;
        [SerializeField]
        private int hiscore;
        [SerializeField]
        private int eatenCherries;
        [SerializeField]
        private int timeLeft;
        [SerializeField]
        private int currentScore;
        #endregion
        
    }
}
