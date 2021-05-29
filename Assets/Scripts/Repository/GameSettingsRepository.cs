using UnityEngine;

namespace PacmanRevival.Repository
{
    [CreateAssetMenu(fileName = "Game settings", menuName = "Pacman Revival/Game settings", order = 1)]
    public class GameSettingsRepository : ScriptableObject
    {
        #region Properties
        public float PacGuyMovementSpeed
        {
            get => pacGuyMovementSpeed;
        }
        public float GhostMovementSpeedRandomizer
        {
            get => ghostMovementSpeedRandomizer;
        }
        public float PersecutionRange {
            get => persecutionRange;
        }
        public float SpecialCherryMovementSpeed
        {
            get => specialCherryMovementSpeed;
        }
        public float SpecialCherrySpawnRate
        {
            get => specialCherrySpawnRate;
        }
        public float SpecialCherryDuration
        {
            get => specialCherryDuration;
        }
        public int CherryConsumptionScore
        {
            get => cherryConsumptionScore;
        }
        public int GhostConsumptionScore
        {
            get => ghostConsumptionScore;
        }
        public int SecondsLeftScore
        {
            get => secondsLeftScore;
        }
        public int TotalTimeInSeconds
        {
            get => totalTimeInSeconds;
        }
        #endregion

        #region Backing Fields
        [Header("PacGuy Movement")]
        [SerializeField]
        private float pacGuyMovementSpeed;

        [Header("Ghost Movement")]
        [SerializeField]
        private float ghostMovementSpeedRandomizer;
        [SerializeField]
        private float persecutionRange;
        
        [Header("Special Cherries")]
        [SerializeField]
        private float specialCherryMovementSpeed;
        [SerializeField]
        private float specialCherrySpawnRate;
        [SerializeField]
        private float specialCherryDuration;
        [SerializeField]

        [Header("Score")]
        private int cherryConsumptionScore;
        [SerializeField]
        private int ghostConsumptionScore;
        [SerializeField]
        private int secondsLeftScore;
        [SerializeField]

        [Header("Time")]
        private int totalTimeInSeconds;
        #endregion
    }
}
