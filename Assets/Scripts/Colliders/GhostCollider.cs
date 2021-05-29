using UnityEngine;

namespace PacmanRevival.Colliders
{
    public class GhostCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<PacGuyCollider>().testCollisionWithGhost(GetComponent<Collider>());
            }
        }
    }
}
