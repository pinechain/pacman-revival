using UnityEngine;

using PacmanRevival.Character.PacGuy;

namespace PacmanRevival.Colliders
{
    public class PortalCollider : MonoBehaviour
    {
        public Vector3 Destination
        {
            get => destination.transform.parent.position;
        }

        [SerializeField]
        private PortalCollider destination;
    }
}
