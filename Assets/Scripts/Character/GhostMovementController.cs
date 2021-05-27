using UnityEngine;
using UnityEngine.AI;

using PacmanRevival.Repository;
using Bladengine.Enumerations;

namespace PacmanRevival.Character
{
    public class GhostMovementController : MonoBehaviour
    {
        #region Destination
        [Header("Destination")]
        [SerializeField]
        private PacGuyMovementController pacGuy;
        
        [SerializeField]
        private Vector3[] visitingPoints = new Vector3[5];

        private int currentVisitingPoint = 0;
        #endregion

        #region Repositories
        [Header("Repositories")]
        [SerializeField]
        private GameDataRepository gameData;
        [SerializeField]
        private GameSettingsRepository gameSettings;
        #endregion

        #region Attributes
        [SerializeField]
        private OrientationType orientationAxis;
        #endregion

        private NavMeshAgent navMeshAgent;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (gameData.IsRunning)
            {
                navMeshAgent.speed = randomizeMovementSpeed();
                navMeshAgent.destination = determineNextDestination();
            }
        }

        private float randomizeMovementSpeed() => Random.Range(gameSettings.PacGuyMovementSpeed - gameSettings.GhostMovementSpeedRandomizer, gameSettings.PacGuyMovementSpeed + gameSettings.GhostMovementSpeedRandomizer);
    
        private Vector3 determineNextDestination() => pacGuyIsNear()? pacGuy.transform.position : determineNextVisitingPoint();

        private bool pacGuyIsNear() => Vector3.Distance(transform.position, pacGuy.transform.position) < gameSettings.PersecutionRange;
    
        private Vector3 determineNextVisitingPoint()
        {
            if (isInDestination())
            {
                currentVisitingPoint = currentVisitingPoint == visitingPoints.Length-1? 0 : currentVisitingPoint + 1;
            }

            return visitingPoints[currentVisitingPoint];
        }

        private bool isInDestination() => orientationAxis == OrientationType.XY ?
                transform.position.x == visitingPoints[currentVisitingPoint].x && transform.position.y == visitingPoints[currentVisitingPoint].y :
                transform.position.x == visitingPoints[currentVisitingPoint].x && transform.position.z == visitingPoints[currentVisitingPoint].z;
    }
}
