using UnityEngine;

namespace Assets.Scripts.Game.Views
{
public abstract class ABaseUIView : MonoBehaviour
    {
        #region Public Methods
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
        #endregion
    }
}
