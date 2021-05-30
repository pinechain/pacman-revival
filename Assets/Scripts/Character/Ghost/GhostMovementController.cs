using UnityEngine;
using UnityEngine.AI;

using Bladengine.Enumerations;

using PacmanRevival.Character.PacGuy;
using PacmanRevival.Enumerations.Character;

namespace PacmanRevival.Character.Ghost
{
    public class GhostMovementController
    {
        #region Destination
        private PacGuyMovementController pacGuy;
        private Vector3 origin;
        private Vector3[] visitingPoints = new Vector3[5];
        #endregion

        #region Attributes
        private Transform ghostTransform;
        private NavMeshAgent navMeshAgent;
        #endregion

        #region State
        private int currentVisitingPoint = 0;
        #endregion

        public GhostMovementController(GameObject gameObject, Vector3 origin, Vector3[] visitingPoints, PacGuyMovementController pacGuy)
        {
            this.origin = origin;
            this.visitingPoints = visitingPoints;
            this.pacGuy = pacGuy;

            ghostTransform = gameObject.transform;
            navMeshAgent = ghostTransform.GetComponent<NavMeshAgent>();
        }

        public void setupMovement(float pacGuyMovementSpeed, float speedRandomizer, GhostState ghostState, Orientation2DType orientationAxis)
        {
            navMeshAgent.speed = randomizeMovementSpeed(pacGuyMovementSpeed, speedRandomizer);
            navMeshAgent.destination = getNextDestination(ghostState, orientationAxis);
        }

        public void stop() => navMeshAgent.isStopped = true;
        public void resume() => navMeshAgent.isStopped = false;

        private float randomizeMovementSpeed(float pacGuyMovementSpeed, float speedRandomizer) => Random.Range(pacGuyMovementSpeed - speedRandomizer, pacGuyMovementSpeed + speedRandomizer);

        private Vector3 getNextDestination(GhostState ghostState, Orientation2DType orientationAxis)
        {
            switch (ghostState)
            {
                case GhostState.Standard:
                    return getNextStandardDestination(orientationAxis);
                case GhostState.Afraid:
                    return getNextAfraidDestination();
                case GhostState.Dead:
                    return getNextDeadDestination();
                case GhostState.Brave:
                    return getNextBraveDestination();
            }

            return origin;
        }

        private Vector3 getNextStandardDestination(Orientation2DType orientationAxis)
        {
            if (isInDestination(orientationAxis))
            {
                currentVisitingPoint = currentVisitingPoint == visitingPoints.Length - 1 ? 0 : currentVisitingPoint + 1;
            }

            return visitingPoints[currentVisitingPoint];
        }

        private Vector3 getNextAfraidDestination() => pacGuy.transform.position * -1;

        private Vector3 getNextDeadDestination() => origin;

        private Vector3 getNextBraveDestination() => pacGuy.transform.position;

        private bool isInDestination(Orientation2DType orientationAxis) => orientationAxis == Orientation2DType.XY ?
                ghostTransform.position.x == visitingPoints[currentVisitingPoint].x && ghostTransform.position.y == visitingPoints[currentVisitingPoint].y :
                ghostTransform.position.x == visitingPoints[currentVisitingPoint].x && ghostTransform.position.z == visitingPoints[currentVisitingPoint].z;
    }
}
