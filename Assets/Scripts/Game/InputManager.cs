using System;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class InputManager : MonoBehaviour
    {
        #region Singleton
        public static InputManager Instance { get; private set; }
        #endregion

        #region Fields
        private Vector2 startTouchPosition;
        private Vector2 currentTouchPosition;
        private Vector2 endTouchPosition;

        private bool stopTouch = false;
        #endregion

        #region Properties
        public Vector2 SwipeDelta { get; private set; }
        public Vector2 SwipeDeltaScreenRatio => new Vector2(SwipeDelta.x / Screen.width, SwipeDelta.y / Screen.height);
        public Vector2 SwipeDirection => SwipeDelta.normalized;
        #endregion

        #region Events
        public event Action OnFire;
        public event Action<int> OnSwitchWeapon;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            SwipeDelta = Vector2.zero;

            if (Input.GetMouseButtonDown(0))
            {
                stopTouch = false;
                startTouchPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                currentTouchPosition = Input.mousePosition;
                SwipeDelta = currentTouchPosition - startTouchPosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                stopTouch = true;
                endTouchPosition = Input.mousePosition;
                SwipeDelta = endTouchPosition - startTouchPosition;
            }

            if (stopTouch)
            {
                startTouchPosition = currentTouchPosition = endTouchPosition = Vector2.zero;
            }
        }
        #endregion

        #region Public Methods
        public float SwipeDirectionX()
        {
            var driftDelta = SwipeDirection.x;

            if (driftDelta > 0)
            {
                driftDelta = 1;
            }
            if (driftDelta < 0)
            {
                driftDelta = -1;
            }

            return driftDelta;
        }
        #endregion
    }
}
