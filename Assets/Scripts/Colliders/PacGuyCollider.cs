using UnityEngine;

namespace PacmanRevival.Colliders
{
    public class PacGuyCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            testCollisionWithCherry(other);
        }

        private void testCollisionWithCherry(Collider other)
        {
            if (other.gameObject.tag == "Cherry") 
            {
                other.gameObject.SetActive(false);
            }
        }
    }
}
