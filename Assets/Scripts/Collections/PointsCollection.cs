using UnityEngine;

namespace PacmanRevival.Collections
{
    [CreateAssetMenu(fileName = "Points collection", menuName = "Pacman Revival/Points collection", order = 1)]
    public class PointsCollection : ScriptableObject
    {
        public Vector3[] VisitingPoints
        {
            get => visitingPoints;
        }

        [SerializeField]
        private Vector3[] visitingPoints = new Vector3[5];
    }
}
