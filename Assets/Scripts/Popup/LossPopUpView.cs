using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Popup
{
    public class LossPopUpView : PopupView
    {
        //  MEMBERS
        //      For Editor
        [SerializeField] private TMP_Text _infoText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;
        //      Private
        private bool isInputEnabled;

        //  METHODS
        override public void Open()
        {
            base.Open();
        }

        override protected void EnableInput()
        {
            if (isInputEnabled == false)
            {
                isInputEnabled = true;
                _menuButton.onClick.AddListener(OnBackButtonClick);
                _restartButton.onClick.AddListener(OnRetryButtonClick);
            }
        }

        override protected void DisableInput()
        {
            if (isInputEnabled == true)
            {
                isInputEnabled = false;
                _menuButton.onClick.RemoveListener(OnBackButtonClick);
                _restartButton.onClick.RemoveListener(OnRetryButtonClick);
            }
        }

        private void OnRetryButtonClick()
        {
            GameEvents.ClickLevelRestart();
        }

        private void OnBackButtonClick()
        {
            GameEvents.ClickGotoMenu();
        }

    }
}
