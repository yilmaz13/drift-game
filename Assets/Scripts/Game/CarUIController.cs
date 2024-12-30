using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Game;

public class CarUIController : MonoBehaviour
{
    [SerializeField] private Image carImage;
    [SerializeField] private Text carNameText;
    [SerializeField] private Button selectButton;

    private CarDataSO _carData;
    private System.Action<CarDataSO> _onCarSelected;

    public void Initialize(CarDataSO carData, System.Action<CarDataSO> onCarSelected)
    {
        _carData = carData;
        _onCarSelected = onCarSelected;

        carNameText.text = _carData.carName;
        carImage.sprite = _carData.carSprite;

        selectButton.onClick.AddListener(OnSelectButtonClicked);
    }

    private void OnSelectButtonClicked()
    {
        _onCarSelected?.Invoke(_carData);
    }
}
