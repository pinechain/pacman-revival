using UnityEngine;

using PacmanRevival.Repository;

namespace PacmanRevival.Colliders
{
    public class PacGuyCollider : MonoBehaviour
    {
        [SerializeField]
        private GameDataRepository gameData;
        [SerializeField]
        private GameSettingsRepository gameSettings;

        private void OnTriggerEnter(Collider other)
        {
            testCollisionWithCherry(other);
        }

        private void testCollisionWithCherry(Collider other)
        {
            if (other.gameObject.tag == "Cherry")
            {
                other.gameObject.SetActive(false);
                gameData.CurrentScore += gameSettings.CherryConsumptionScore;
                gameData.EatenCherries++;
            }
        }
    }
}
