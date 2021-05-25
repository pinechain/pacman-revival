using System;
using UnityEngine;

using PacmanRevival.Repository;

using Bladengine.Character;

namespace PacmanRevival.Character
{
    public class PacGuyMovementController : BaseMovementController
    {
        #region Repositories
        [Header("Repositories")]
        [SerializeField]
        private GameSettingsRepository gameSettings;
        [SerializeField]
        private GameDataRepository gameData;
        #endregion

        protected override float calculateMoveSpeed() => gameData.IsRunning? gameSettings.PacGuyMovementSpeed : 0;
    }
}
