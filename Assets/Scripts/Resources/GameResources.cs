using UnityEngine;

[CreateAssetMenu(fileName = "GameResources", menuName = "ScriptableObjects/GameResources", order = 0)]

public class GameResources : ScriptableObject
{
    [Header("Config")]
    [SerializeField] private float _followCameraSmootTime;

    public float FollowCameraSmootTime => _followCameraSmootTime;
}
