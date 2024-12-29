using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIView : MonoBehaviour
{
    //  MEMBERS
    //      From Editor
    [SerializeField] private Button _backButton;
    [SerializeField] private TMP_Text _levelNameLabel;
    [SerializeField] private TMP_Text _fpsLabel;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void LoadLevel(int level)
    {
        _levelNameLabel.text = "Level" + level;
        _backButton.onClick.AddListener(OnBackButtonClick);
    }
    public void UnloadLevel()
    {
        _levelNameLabel.text = "Level --";
        _backButton.onClick.RemoveListener(OnBackButtonClick);
    }
  
    private void OnBackButtonClick()
    {
        GameEvents.ClickGotoMenu();
    }

    public void SetFPSCount(float fps)
    {
        _fpsLabel.text = "FPS: " + fps;
    }
}
