using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game.Views
{
    public class MainMenuView : ABaseUIView
    {
        #region Fields
        [SerializeField] private Button _playGameButton;
        [SerializeField] private TextMeshProUGUI _levelText;
        #endregion

        #region Public Methods
        public override void Show()
        {
            base.Show();
            _playGameButton.onClick.AddListener(PlayGame);
        }

        public override void Hide()
        {
            base.Hide();
            _playGameButton.onClick.RemoveListener(PlayGame);
        }

        public void PlayGame()
        {
            GameEvents.ClickGotoGameScene();
        }

        public void DisplayLevelText(int levelAmount)
        {
            _levelText.text = levelAmount.ToString();
        }

        #endregion
    }
}
