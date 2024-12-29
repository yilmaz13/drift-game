using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game.Views
{
    public class LoadingUIView : ABaseUIView
    {
        #region Fields
        [SerializeField] private TMP_Text _loadingText;
        [SerializeField] private Slider _loadingSlider;
        [SerializeField] private Transform _hintUIViewContanier;
        #endregion

        #region Public Methods
        public override void Hide()
        {
            base.Hide();
            _loadingSlider.value = 0;
        }

        public void SetLoadingSlider(float time)
        {
            _loadingSlider.DOValue(1, time);
        }
        #endregion
    }
}