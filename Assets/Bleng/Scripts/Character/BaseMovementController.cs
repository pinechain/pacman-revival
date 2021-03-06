using System;

using UnityEngine;

using Bladengine.Input;
using Bladengine.Enumerations;

namespace Bladengine.Character
{
    public abstract class BaseMovementController : MonoBehaviour
    {
        #region Properties
        public Orientation2DType Orientation
        {
            get => orientationAxis;
        }
        #endregion

        #region Attributes
        [SerializeField]
        private Orientation2DType orientationAxis;
        #endregion

        #region Movement
        private InputMap inputMap;
        private CharacterController characterController;
        #endregion

        #region State
        private Vector3 movingDirection = Vector2.zero;
        #endregion

        #region MonoBehaviour
        protected void Awake()
        {
            characterController = GetComponent<CharacterController>();
            inputMap = new InputMap();

            inputMap.Character.Move.performed += ctx => move(ctx.ReadValue<Vector2>());
            inputMap.Character.Move.canceled += _ => halt();
        }

        protected void Update()
        {
            characterController.Move(movingDirection * Time.deltaTime * calculateMoveSpeed());
        }

        protected void OnEnable()
        {
            inputMap.Enable();
            doOnEnable();
        }

        protected void OnDisable()
        {
            inputMap.Disable();
            doOnDisable();
        }
        #endregion

        #region Movement
        protected void move(Vector2 direction)
        {
            switch (orientationAxis)
            {
                case Orientation2DType.XY:
                    movingDirection = direction;
                    break;
                case Orientation2DType.XZ:
                    movingDirection = new Vector3(direction.x, 0, direction.y);
                    break;
            }
            transform.forward = movingDirection;
        }

        protected void halt()
        {
            movingDirection = Vector2.zero;
        }

        protected void teleportTo(Vector3 destination)
        {
            characterController.enabled = false;
            transform.position = destination;
            characterController.enabled = true;
        }
        #endregion

        #region Inheritance
        protected virtual void doOnEnable() {}
        protected virtual void doOnDisable() {}
        protected abstract float calculateMoveSpeed();
        #endregion
    }
}
