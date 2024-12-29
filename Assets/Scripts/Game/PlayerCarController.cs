using DG.Tweening;
using System;
using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    [SerializeField] private DetectionCollider detectionColliderLetf;
    [SerializeField] private DetectionCollider detectionColliderRight;
    [SerializeField] private DetectionCollider detectionColliderBody;
    [SerializeField] private Material rutsMaterial;
    [SerializeField] private BoxCollider boxCollider;

    [SerializeField] private GameObject partsOtherWheels;
    [SerializeField] private GameObject partsAllCar;
    [SerializeField] private GameObject[] wheels;
    [SerializeField] private GameObject[] rims;
    [SerializeField] private Transform[] rutTransforms;

    [SerializeField] private TrailRenderer trailRendererLeft;
    [SerializeField] private TrailRenderer trailRendererRight;

    //TODO go to gameresource
    private bool isStart = false;
    private float speed = 20f;
    private float turnSpeed = 50f;

    private float resetRotationTime = 1f;
    private float resetRotationTimer = 0f;

    private float drifrMulitplier = 1.2f;
    public float driftAngle = 10f;

    private float _driftMultiplierInitial = 1.01f;
    private float _driftMultiplierDecay = 0.99f;
    private float _swipeMultiplierDecay = 0.99f;
    private float _rotationLerpSpeed = 4f;

    private float _maxDriftDelta = 1.2f;
    private float _minSwipeDelta = 0.02f;
    private float swipeDelta = 0;
    private float driftDelta = 0;

    private float _trailEmissionThreshold = 0.05f;

    Tween[] rotationWheelTweens;

    public void Initialize()
    {
        isStart = true;

        rotationWheelTweens = new Tween[4];
        SetAllWheelsRotationTween(1f / 20f);

        trailRendererLeft.Clear();
        trailRendererLeft.AddPosition(rutTransforms[0].position);

        detectionColliderLetf.OnCollisionDetected += ClosePass;
        detectionColliderRight.OnCollisionDetected += ClosePass;

        detectionColliderBody.OnCollisionDetected += CrashForce;

    }

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

    void Update()
    {
        if (!isStart) return;

        MoveCar();
        RotateCarWithAngle();
        SteerCar();
    }

    private void MoveCar()
    {
        float move = speed * Time.deltaTime * 1.2f;
        transform.Translate(Vector3.forward * move);
    }

    private void RotateCarWithAngle()
    {
        if (InputManager.Instance.SwipeDeltaScreenRatio.x == 0)
        {
            driftDelta *= drifrMulitplier;
            driftDelta = Mathf.Clamp(driftDelta, -_maxDriftDelta, _maxDriftDelta);


            resetRotationTimer += Time.deltaTime;
            if (resetRotationTimer >= resetRotationTime)
            {
                drifrMulitplier = _driftMultiplierDecay;
                resetRotationTimer = 0;
            }
        }
        else
        {
            drifrMulitplier = _driftMultiplierInitial;
            driftDelta = InputManager.Instance.SwipeDirection.x;

            driftDelta = Mathf.Sign(InputManager.Instance.SwipeDirection.x);           

            resetRotationTimer = 0f;
        }
        RotateCarWithAngle(driftDelta * driftAngle * -1, driftDelta);
    }
    
    public void RotateCarWithAngle(float angle, float direction)
    {
        var targetYRotation = angle;

        Quaternion currentRot = partsAllCar.transform.rotation;
        Quaternion targerRot = Quaternion.Euler(partsAllCar.transform.rotation.eulerAngles.x, targetYRotation, partsAllCar.transform.rotation.eulerAngles.z);
        Quaternion smoothRot = Quaternion.LerpUnclamped(currentRot, targerRot, Time.deltaTime * _rotationLerpSpeed);
        partsAllCar.transform.rotation = smoothRot;

        wheels[0].transform.localRotation = Quaternion.Inverse(smoothRot);
        wheels[1].transform.localRotation = Quaternion.Inverse(smoothRot);

        currentRot = partsOtherWheels.transform.localRotation;
        targerRot = new Quaternion(partsOtherWheels.transform.localRotation.eulerAngles.x,
                                   partsOtherWheels.transform.localRotation.eulerAngles.y,
                                   -0.100f * direction,
                                   1);
        smoothRot = Quaternion.LerpUnclamped(currentRot, targerRot, Time.deltaTime * _rotationLerpSpeed);


        partsOtherWheels.transform.localRotation = smoothRot;

        UpdatedRutMaterial(targetYRotation);
        UpdateTrailEmission(targetYRotation);
    }

    private void UpdatedRutMaterial(float targetYRotation)
    {
        rutsMaterial.color = new Color(rutsMaterial.color.r,
                                      rutsMaterial.color.g,
                                      rutsMaterial.color.b,
                                      Math.Abs(targetYRotation / 60));
    }
    private void UpdateTrailEmission(float targetYRotation)
    {
        bool shouldEmit = Math.Abs(targetYRotation / 60) > _trailEmissionThreshold;
        trailRendererLeft.emitting = shouldEmit;
        trailRendererRight.emitting = shouldEmit;
    }
    private void SteerCar()
    {
        //input gelmesse hareketi smootlaþtýr
        if (InputManager.Instance.SwipeDeltaScreenRatio.x == 0)
        {
            swipeDelta *= _swipeMultiplierDecay;
            //kaymayý kontrol et
            if (swipeDelta <= _minSwipeDelta && swipeDelta >= -_minSwipeDelta)
            {
                //kaymayý durdur
                swipeDelta = 0;
                return;
            }
        }
        else
        {
            swipeDelta = InputManager.Instance.SwipeDeltaScreenRatio.x;
        }

        float steeringAmount = swipeDelta * turnSpeed * Time.deltaTime / 2;
        Vector3 newPosition = transform.position + new Vector3(steeringAmount, 0, 0);

        //TODO go to gameresource
        newPosition.x = Mathf.Clamp(newPosition.x, -3, 3);

        transform.position = newPosition;
    }

    private void CrashForce(GameObject gameObject)
    {

        isStart = false;

        gameObject.SetActive(false);

        GameEvents.EndGame(false);  
        return;
    }

    private void ClosePass(GameObject gameObject)
    {
        Debug.Log("Close Pass");
    }

    private void OnDestroy()
    {
        detectionColliderLetf.OnCollisionDetected -= ClosePass;
        detectionColliderRight.OnCollisionDetected -= ClosePass;

        detectionColliderBody.OnCollisionDetected -= CrashForce;
    }
}