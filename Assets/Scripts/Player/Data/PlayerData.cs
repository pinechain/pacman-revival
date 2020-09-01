using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Data
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Player/Data/Player Data", order = 1)]
    public class PlayerData : ScriptableObject
    {
        #region Properties
        public float MovementSpeed
        {
            get => movementSpeed;
            private set => movementSpeed = value;
        }
        #endregion

        [SerializeField]
        private float movementSpeed = 1.0f;
    }
}