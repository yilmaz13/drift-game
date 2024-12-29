using System;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class PlayerCarController : BaseCarController
    {
        #region Fields
        //TODO birden fazla car olabilir Initialize'da set edilebilir   
        [SerializeField] private CarParametersSO _carParametersSO;
        [SerializeField] private DetectionCollider _detectionColliderLetf;
        [SerializeField] private DetectionCollider _detectionColliderRight;
        [SerializeField] private DetectionCollider _detectionColliderBody;

        [SerializeField] private Transform[] _rutTransforms;
        [SerializeField] private TrailRenderer _trailRendererLeft;
        [SerializeField] private TrailRenderer _trailRendererRight;

        //TODO go to gameresource
        private bool isStart = false;
        private float _turnSpeed = 50f;

        private float _resetRotationTime;
        private float _drifrMulitplier;
        private float _driftMultiplierInitial;
        private float _driftMultiplierDecay;
        private float _swipeMultiplierDecay;
        private float _rotationLerpSpeed;
        private float _maxDriftDelta;
        private float _minSwipeDelta;
        private float _trailEmissionThreshold;
        private float _driftAngle;

        private float swipeDelta = 0;
        private float driftDelta = 0;
        private float _resetRotationTimer = 0f;
        #endregion

        #region Public Methods
        public override void Initialize()
        {
            base.Initialize();
            isStart = true;
            
            _resetRotationTime = _carParametersSO.resetRotationTime;
            _drifrMulitplier = _carParametersSO.drifrMulitplier;
            _driftMultiplierInitial = _carParametersSO.driftMultiplierInitial;
            _driftMultiplierDecay = _carParametersSO.driftMultiplierDecay;
            _swipeMultiplierDecay = _carParametersSO.swipeMultiplierDecay;
            _rotationLerpSpeed = _carParametersSO.rotationLerpSpeed;
            _maxDriftDelta = _carParametersSO.maxDriftDelta;
            _minSwipeDelta = _carParametersSO.minSwipeDelta;
            _trailEmissionThreshold = _carParametersSO.trailEmissionThreshold;
            _driftAngle = _carParametersSO.driftAngle;

            _trailRendererLeft.Clear();
            _trailRendererLeft.AddPosition(_rutTransforms[0].position);

            _detectionColliderLetf.OnCollisionDetected += ClosePass;
            _detectionColliderRight.OnCollisionDetected += ClosePass;
            _detectionColliderBody.OnCollisionDetected += CrashForce;
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
        #endregion

        #region Unity Methods
        void Update()
        {
            if (!isStart) return;

            MoveCar();
            RotateCarWithAngle();
            SteerCar();
        }
       
        #endregion

        #region Private Methods
        private void MoveCar()
        {
            float move = speed * Time.deltaTime * 1.2f;
            transform.Translate(Vector3.forward * move);
        }

        private void RotateCarWithAngle()
        {
            if (InputManager.Instance.SwipeDeltaScreenRatio.x == 0)
            {
                driftDelta *= _drifrMulitplier;
                driftDelta = Mathf.Clamp(driftDelta, -_maxDriftDelta, _maxDriftDelta);

                _resetRotationTimer += Time.deltaTime;
                if (_resetRotationTimer >= _resetRotationTime)
                {
                    _drifrMulitplier = _driftMultiplierDecay;
                    _resetRotationTimer = 0;
                }
            }
            else
            {
                _drifrMulitplier = _driftMultiplierInitial;
                driftDelta = InputManager.Instance.SwipeDirection.x;

                driftDelta = Mathf.Sign(InputManager.Instance.SwipeDirection.x);

                _resetRotationTimer = 0f;
            }
            RotateCarWithAngle(driftDelta * _driftAngle * -1, driftDelta);
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
            _trailRendererLeft.emitting = shouldEmit;
            _trailRendererRight.emitting = shouldEmit;
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

            float steeringAmount = swipeDelta * _turnSpeed * Time.deltaTime / 2;
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
        #endregion
        private void OnDestroy()
        {
            _detectionColliderLetf.OnCollisionDetected -= ClosePass;
            _detectionColliderRight.OnCollisionDetected -= ClosePass;
            _detectionColliderBody.OnCollisionDetected -= CrashForce;
        }
    }
}