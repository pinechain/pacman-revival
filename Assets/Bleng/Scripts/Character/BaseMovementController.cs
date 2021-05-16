using System;

using UnityEngine;

using Bladengine.Input;

namespace Bladengine.Character
{
    public class BaseMovementController : MonoBehaviour
    {
        #region Movement
        private InputMap inputMap;
        private CharacterController characterController;
        #endregion

        #region Data
        [SerializeField]
        private float movementSpeed = 2.0f;
        #endregion

        #region State
        private Vector2 movingDirection = Vector2.zero;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            inputMap = new InputMap();

            inputMap.Character.Move.performed += ctx => move(ctx.ReadValue<Vector2>());
            inputMap.Character.Move.canceled += _ => halt();
        }

        private void Update()
        {
            characterController.Move(movingDirection * Time.deltaTime * movementSpeed); 
        }

        private void OnEnable()
        {
            inputMap.Enable();
        }

        private void OnDisable()
        {
            inputMap.Disable();
        }
        #endregion

        private void move(Vector2 direction)
        {
            movingDirection = direction;
            transform.forward = movingDirection;
        }

        private void halt()
        {
            movingDirection = Vector2.zero;
        }
    }
}
