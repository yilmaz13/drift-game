using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Resources
{
    [CreateAssetMenu(fileName = "GameResources", menuName = "ScriptableObjects/GameResources", order = 0)]
    public class GameResources : ScriptableObject
    {
        #region Fields
        [Header("Config")]
        [SerializeField] private float _followCameraSmootTime;
        [SerializeField] private GameObject _playerCar;
        [SerializeField] private GameObject _aiCar;
        [SerializeField] private GameObject _roadController;
        [SerializeField] private List<CarDataSO> _carDataList;
        #endregion

        #region Properties
        public float FollowCameraSmootTime => _followCameraSmootTime;
        public GameObject PlayerCar => _playerCar;
        public GameObject AiCar => _aiCar;
        public GameObject RoadController => _roadController;
        public List<CarDataSO> CarDataList => _carDataList;
        #endregion
    }
}
