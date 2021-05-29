using System.Collections;

using UnityEngine;

using PacmanRevival.Repository;
using PacmanRevival.Collections;
using PacmanRevival.Character.PacGuy;
using PacmanRevival.Enumerations.Character;
using PacmanRevival.Enumerations.Data;

using Bladengine.Enumerations;

namespace PacmanRevival.Character.Ghost
{
    public class GhostController : MonoBehaviour
    {
        #region Destination
        [Header("Destination")]
        [SerializeField]
        private Vector3 origin;

        [SerializeField]
        private PointsCollection pointsCollection;

        [SerializeField]
        private PacGuyMovementController pacGuy;
        #endregion

        #region Repositories
        [Header("Repositories")]
        [SerializeField]
        private GameDataRepository gameData;
        [SerializeField]
        private GameSettingsRepository gameSettings;
        #endregion

        #region Attributes
        [Header("2D Orientation")]
        [SerializeField]
        private OrientationType orientationAxis;

        [Header("Appearance")]
        [SerializeField]
        private Material scaredAppearance;

        [SerializeField]
        private Material defaultAppearance;

        [SerializeField]
        private Material deadAppearance;
        #endregion

        #region Controllers
        private GhostStateController stateController;
        private GhostMovementController movementController;
        #endregion

        private void Awake()
        {
            stateController = new GhostStateController(gameObject, new Material[] { defaultAppearance, scaredAppearance, deadAppearance });
            movementController = new GhostMovementController(gameObject, origin, pointsCollection.VisitingPoints, pacGuy);
        }

        private void Update()
        {
            if (gameData.IsRunning)
            {
                adjustPosition();
                switchBrave();
                switchDead();
                movementController.setupMovement(gameSettings.PacGuyMovementSpeed, gameSettings.GhostMovementSpeedRandomizer, stateController.State, orientationAxis);
            }
        }

        private void OnEnable()
        {
            gameData.subscribe(GameDataType.IsRunning, reset);
            gameData.subscribe(GameDataType.SpecialCherryIsConsumed, switchAfraid);
        }

        private void OnDisable()
        {
            gameData.unsubscribe(GameDataType.IsRunning, reset);
            gameData.unsubscribe(GameDataType.SpecialCherryIsConsumed, switchAfraid);
        }

        private bool pacGuyIsNear() => Vector3.Distance(pacGuy.transform.position, transform.position) <= gameSettings.PersecutionRange;

        private void switchBrave()
        {
            if (stateController.State == GhostState.Standard || stateController.State == GhostState.Brave)
            {
                if (stateController.State == GhostState.Standard && pacGuyIsNear())
                {
                    stateController.State = GhostState.Brave;
                }
                else if (stateController.State == GhostState.Brave && !pacGuyIsNear())
                {
                    stateController.State = GhostState.Standard;
                }
            }
        }

        private void switchAfraid()
        {
            if (gameData.SpecialCherryIsConsumed)
            {
                stateController.State = GhostState.Afraid;
            }
            else
            {
                stateController.State = GhostState.Standard;
            }
        }

        private void switchDead()
        {
            if (stateController.State == GhostState.Dead && isInOrigin())
            {
                stateController.State = gameData.SpecialCherryIsConsumed ? GhostState.Afraid : GhostState.Standard;
            }
        }

        public void die()
        {
            stateController.State = GhostState.Dead;
        }

        private bool isInOrigin()
        {
            switch (orientationAxis)
            {
                case OrientationType.XY:
                    return transform.position.x == origin.x && transform.position.y == origin.y;
                case OrientationType.XZ:
                    return transform.position.x == origin.x && transform.position.z == origin.z;
            }

            return false;
        }

        private void adjustPosition()
        {
            switch (orientationAxis)
            {
                case OrientationType.XY:
                    transform.position = new Vector3(transform.position.x, transform.position.y, origin.z);
                    break;
                case OrientationType.XZ:
                    transform.position = new Vector3(transform.position.x, origin.y, transform.position.z);
                    break;
            }
        }

        private void reset() => StartCoroutine(resetCR());

        private IEnumerator resetCR()
        {
            if (!gameData.IsRunning)
            {
                movementController.stop();
                stateController.State = GhostState.Standard;
                yield return new WaitForSeconds(0.5f);
                transform.position = origin;
            }
            else
            {
                movementController.resume();
            }

        }
    }
}
