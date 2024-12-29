using UnityEngine;

namespace Assets.Scripts.Resources
{
    public class UserDataManager : IUserDataManager
    {
        #region Constructor
        public UserDataManager()
        {
            if (!UserLevelExist())
            {
                SetCurrentLevel(0);
            }
        }
        #endregion

        #region Public Methods
        public int CurrentLevel()
        {
            return PlayerPrefs.GetInt(PlayerPrefKeys.CURRENTLEVEL);
        }

        public void SetNextLevel()
        {
            var level = (CurrentLevel() + 1);
            PlayerPrefs.SetInt(PlayerPrefKeys.CURRENTLEVEL, level);
        }

        public void SetCurrentLevel(int level)
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.CURRENTLEVEL, level);
        }

        public bool UserLevelExist()
        {
            return PlayerPrefs.HasKey(PlayerPrefKeys.CURRENTLEVEL);
        }
        #endregion
    }
}
