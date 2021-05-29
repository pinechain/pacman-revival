using System.Collections;
using UnityEngine;

using PacmanRevival.Repository;

using Bladengine.Character;

using PacmanRevival.Enumerations.Data;

namespace PacmanRevival.Character.PacGuy
{
    public class PacGuyMovementController : BaseMovementController
    {
        #region Destination
        [Header("Destination")]
        [SerializeField]
        private Vector3 origin;
        #endregion

        #region Repositories
        [Header("Repositories")]
        [SerializeField]
        private GameSettingsRepository gameSettings;
        [SerializeField]
        private GameDataRepository gameData;
        #endregion

        private void reset() => StartCoroutine(resetCR());

        private IEnumerator resetCR()
        {
            if (!gameData.IsRunning)
            {
                gameData.PacGuyIsDead = true;
                yield return new WaitForSeconds(0.5f);
                transform.position = origin;
                gameData.PacGuyIsDead = false;
            }
        }

        protected override void doOnEnable() => gameData.subscribe(GameDataType.IsRunning, reset);
        protected override void doOnDisable() => gameData.unsubscribe(GameDataType.IsRunning, reset);
        protected override float calculateMoveSpeed() => gameData.IsRunning? gameSettings.PacGuyMovementSpeed : 0;
    }
}
