using System;

namespace Bladengine.Enumerations
{
    public enum ColliderType
    {
        NoCollider = -1,
        StaticCollider = 1,
        RigidbodyCollider = 2,
        KinematicRigidbodyCollider = 3,
        StaticTriggerCollider = 5,
        RigidbodyTriggerCollider = 6,
        KinematicRigidbodyTriggerCollider = 7
    }
}
