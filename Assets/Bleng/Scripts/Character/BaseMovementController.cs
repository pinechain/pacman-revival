using System;

using UnityEngine;

using Bladengine.Input;

namespace Bladengine.Character
{
    public abstract class BaseMovementController : MonoBehaviour
    {
        #region Movement
        private InputMap inputMap;
        private CharacterController characterController;
        #endregion

        #region State
        private Vector2 movingDirection = Vector2.zero;
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
        }

        protected void OnDisable()
        {
            inputMap.Disable();
        }
        #endregion

        #region Movement
        protected void move(Vector2 direction)
        {
            movingDirection = direction;
            transform.forward = movingDirection;
        }

        protected void halt()
        {
            movingDirection = Vector2.zero;
        }
        #endregion

        #region Inheritance
        protected abstract float calculateMoveSpeed();
        #endregion
    }
}
