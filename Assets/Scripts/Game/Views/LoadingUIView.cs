using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUIView : MonoBehaviour
{
    //  MEMBERS
    //      From Editor
    [SerializeField] private TMP_Text _loadingText;
    [SerializeField] private Slider _loadingSlider;
    [SerializeField] private Transform _hintUIViewContanier;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        _loadingSlider.value = 0;
    }

    public void SetLoadingSlider(float time)
    {
        _loadingSlider.DOValue(1, time);
    }

}