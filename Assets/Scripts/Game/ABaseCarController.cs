using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public abstract class BaseCarController : MonoBehaviour
    {
        #region Fields
        // Ortak alanlar
        [SerializeField] protected GameObject partsAllCar;
        [SerializeField] protected GameObject partsOtherWheels;
        [SerializeField] protected GameObject[] wheels;
        [SerializeField] protected GameObject[] rims;
        [SerializeField] protected Material rutsMaterial;

        protected float speed = 20f;
        protected Tween[] rotationWheelTweens;
        #endregion

        #region Public Methods
        public virtual void Initialize()
        {
            rotationWheelTweens = new Tween[4];
            SetAllWheelsRotationTween(1f / 20f);
        }
       
        #endregion

        #region Private Methods
        private void SetAllWheelsRotationTween(float duration)
        {
            for (int i = 0; i < rotationWheelTweens.Length; i++)
            {
                rotationWheelTweens[i]?.Kill();
                rotationWheelTweens[i] = rims[i].transform.DOLocalRotate(new Vector3(360, 0, 0), duration, RotateMode.LocalAxisAdd)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1, LoopType.Incremental);
            }
        }
        #endregion
    }
}
