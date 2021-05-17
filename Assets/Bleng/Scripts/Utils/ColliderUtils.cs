using static Bladengine.Enumerations.ColliderType;
using Bladengine.Enumerations;

using UnityEngine;

namespace Bladengine.Utils
{
    public sealed class ColliderUtils
    {
        private ColliderUtils() { }

        public static ColliderType determineColliderType(GameObject collider)
        {
            Collider colliderComponent = collider.GetComponent<Collider>();
            if (colliderComponent == null) return NoCollider;

            Rigidbody rigidbodyComponent = collider.GetComponent<Rigidbody>();
            return determineColliderType((colliderComponent.isTrigger ? 1 : 0) + (rigidbodyComponent != null ? 2 : 0) + (rigidbodyComponent.isKinematic ? 4 : 0));
        }

        public static ColliderType determineColliderType(int param)
        {
            if (param == 0 || param == 4) param++;
            return (ColliderType) param;
        }
    }
}
