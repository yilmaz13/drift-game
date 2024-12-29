using UnityEngine;

[CreateAssetMenu(fileName = "GameResources", menuName = "ScriptableObjects/GameResources", order = 0)]

public class GameResources : ScriptableObject
{
    [Header("Config")]
    [SerializeField] private float _followCameraSmootTime;
    [SerializeField] private GameObject _playerCar;
    [SerializeField] private GameObject _aiCar;
    [SerializeField] private GameObject _roadController;


    public float FollowCameraSmootTime => _followCameraSmootTime;
    public GameObject PlayerCar => _playerCar;
    public GameObject AiCar => _aiCar;
    public GameObject RoadController => _roadController;
}
