using UnityEngine;

[CreateAssetMenu(fileName = "CarParametersSO", menuName = "ScriptableObjects/CarParametersSO")]
public class CarParametersSO : ScriptableObject
{
    public float resetRotationTime;
    public float drifrMulitplier;
    public float driftMultiplierInitial;
    public float driftMultiplierDecay;
    public float swipeMultiplierDecay;
    public float rotationLerpSpeed;
    public float maxDriftDelta;
    public float minSwipeDelta;
    public float trailEmissionThreshold;
    public float driftAngle;
}
