using UnityEngine;
using PacmanRevival.Enumerations.Character;

namespace PacmanRevival.Character.Ghost
{
    public class GhostStateController
    {
        #region Properties
        public GhostState State
        {
            get => state;
            set => changeState(value);
        }
        #endregion

        #region Appearance
        private Material[] appearances;
        #endregion

        #region Attributes
        private Transform ghostTransform;
        private Transform braveSign;
        #endregion

        #region State
        private GhostState state;
        #endregion

        public GhostStateController(GameObject gameObject, Material[] appearances)
        {
            this.appearances = appearances;
            this.ghostTransform = gameObject.transform;
            this.braveSign = ghostTransform.GetChild(0);
        }

        private void changeState(GhostState state)
        {
            this.state = state;
            if (state == GhostState.Brave)
            {
                braveSign.gameObject.SetActive(true);
            }
            else
            {
                braveSign.gameObject.SetActive(false);
                ghostTransform.GetComponent<MeshRenderer>().material = appearances[(int)state];
            }
        }
    }
}
