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
        public float GhostMovementSpeed
        {
            get =>ghostMovementSpeed;
        }
        public float SpecialCherryMovementSpeed
        {
            get => specialCherryMovementSpeed;
        }
        public float SpecialCherrySpawnRate
        {
            get => specialCherrySpawnRate;
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
        [Header("Movement")]
        [SerializeField]
        private float pacGuyMovementSpeed;
        [SerializeField]
        private float ghostMovementSpeed;
        [SerializeField]

        [Header("Special Cherries")]
        private float specialCherryMovementSpeed;
        [SerializeField]
        private float specialCherrySpawnRate;
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
