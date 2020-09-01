using UnityEngine;
using UnityEngine.InputSystem;

using Player.Data;

namespace Player.Input
{
    public class InputHandler : MonoBehaviour
    {
        #region Assignable references
        [SerializeField]
        private PlayerData playerData;
        #endregion

        #region Non-assignable references
        private Rigidbody pacmanBody;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            pacmanBody = transform.GetComponentInChildren<Rigidbody>();
        }
        #endregion

        #region Input callbacks
        public void move(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                move((Vector2)ctx.ReadValue<Vector2>());
            }
        }
        #endregion

        #region Internal methods
        public void move(Vector2 direction)
        {
            //TODO: enhance rotation code matemagically!
            //TODO: make rotation smooth!
            if (direction == Vector2.right)
            {
                pacmanBody.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (direction == Vector2.up)
            {
                pacmanBody.transform.eulerAngles = new Vector3(0, 0, 90);
            }
            else if (direction == Vector2.left)
            {
                pacmanBody.transform.eulerAngles = new Vector3(0, 0, 180);
            }
            else if (direction == Vector2.down)
            {
                pacmanBody.transform.eulerAngles = new Vector3(0, 0, 270);
            }

            pacmanBody.velocity = direction * playerData.MovementSpeed;
        }
        #endregion
    }
}