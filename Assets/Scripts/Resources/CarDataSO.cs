using UnityEngine;

[CreateAssetMenu(fileName = "CarData", menuName = "ScriptableObjects/CarDataSO", order = 1)]
public class CarDataSO : ScriptableObject
{
    public string carName;
    public Sprite carSprite;
    public GameObject carPrefab;
}
