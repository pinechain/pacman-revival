using System.Collections;

using UnityEngine;

using Bladengine.Enumerations;

using PacmanRevival.Repository;
using PacmanRevival.Character.Ghost;
using PacmanRevival.Character.PacGuy;

namespace PacmanRevival.Colliders
{
    public class PacGuyCollider : MonoBehaviour
    {
        [SerializeField]
        private GameDataRepository gameData;
        [SerializeField]
        private GameSettingsRepository gameSettings;
        [SerializeField]
        private float remainingSpecialCherryEffect = 0;

        private void OnTriggerEnter(Collider other)
        {
            testCollisionWithCherry(other);
            testCollisionWithSpecialCherry(other);
            testCollisionWithGhost(other);
        }

        private void OnTriggerExit(Collider other)
        {
            testCollisionWithPortal(other);
        }

        private void testCollisionWithCherry(Collider other)
        {
            if (other.gameObject.tag == "Cherry")
            {
                consumeCherry(other.gameObject);
            }
        }

        private void testCollisionWithSpecialCherry(Collider other)
        {
            if (other.gameObject.tag == "Special Cherry")
            {
                consumeCherry(other.gameObject);
                StartCoroutine(consumeSpecialCherryCR());
            }
        }

        public void testCollisionWithGhost(Collider other)
        {
            if (other.gameObject.tag == "Ghost")
            {
                if (gameData.SpecialCherryIsConsumed)
                {
                    gameData.CurrentScore += gameSettings.GhostConsumptionScore;
                    other.GetComponent<GhostController>().die();
                }
                else
                {
                    gameData.PacGuyIsDead = true;
                }
            }
        }

        public void testCollisionWithPortal(Collider other)
        {
            PortalCollider portal = other.GetComponent<PortalCollider>();
            if (portal != null)
            {
                switch (GetComponent<PacGuyMovementController>().Orientation)
                {
                    case Orientation2DType.XY:
                        GetComponent<PacGuyMovementController>().teleport(new Vector3(portal.Destination.x, portal.Destination.y, transform.position.z));
                        break;
                    case Orientation2DType.XZ:
                        GetComponent<PacGuyMovementController>().teleport(new Vector3(portal.Destination.x, transform.position.y, portal.Destination.z));
                        break;
                }
            }
        }

        private void consumeCherry(GameObject cherry)
        {
            cherry.SetActive(false);
            gameData.CurrentScore += gameSettings.CherryConsumptionScore;
            gameData.EatenCherries++;
        }

        private IEnumerator consumeSpecialCherryCR()
        {
            gameData.SpecialCherryIsConsumed = true;
            remainingSpecialCherryEffect += gameSettings.SpecialCherryDuration;
            yield return new WaitForSeconds(gameSettings.SpecialCherryDuration);
            remainingSpecialCherryEffect -= gameSettings.SpecialCherryDuration;
            if (remainingSpecialCherryEffect == 0)
            {
                gameData.SpecialCherryIsConsumed = false;
            }
        }
    }
}
