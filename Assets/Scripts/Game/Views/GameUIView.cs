using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game.Views
{
    public class GameUIView : ABaseUIView
    {
        #region Fields
        [SerializeField] private Button _backButton;
        [SerializeField] private TMP_Text _levelNameLabel;
        [SerializeField] private TMP_Text _fpsLabel;
        #endregion

        #region Public Methods
        public void LoadLevel(int level)
        {
            _levelNameLabel.text = "Level " + level;
            _backButton.onClick.AddListener(OnBackButtonClick);
        }

        public void UnloadLevel()
        {
            _levelNameLabel.text = "Level --";
            _backButton.onClick.RemoveListener(OnBackButtonClick);
        }

        public void SetFPSCount(float fps)
        {
            _fpsLabel.text = "FPS: " + fps;
        }
        #endregion

        #region Private Methods
        private void OnBackButtonClick()
        {
            GameEvents.ClickGotoMenu();
        }
        #endregion
    }
}
