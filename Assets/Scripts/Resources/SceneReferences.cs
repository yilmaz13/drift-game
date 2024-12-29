using UnityEngine;
using Assets.Scripts.Popup;

namespace Assets.Scripts.Resources
{
    public class SceneReferences : MonoBehaviour
    {
        //  MEMBERS
        public GameObject ViewContainer { get { return _viewContainer; } }
        public GameObject UIViewContainer { get { return _uiViewContainer; } }
        public GameObject PoolContainer { get { return _poolContainer; } }
        public PopupManager PopupManager { get { return _popupManager; } }
        public SpawnManager SpawnManager { get { return _spawnManager; } }
        public Camera MainCam { get { return _mainCam; } }

        //      From Editor
        [SerializeField] public GameObject _viewContainer;
        [SerializeField] public GameObject _uiViewContainer;
        [SerializeField] public GameObject _poolContainer;

        [SerializeField] private Camera _mainCam;
        [SerializeField] private PopupManager _popupManager;
        [SerializeField] SpawnManager _spawnManager;
    }
}
