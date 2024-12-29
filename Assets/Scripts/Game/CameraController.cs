using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables

    [SerializeField] private Vector3 _offset;

    private Transform _targetTransform;
    private Transform _transform;
    private float _smoothTime = 0.1f;
    private float _offsetY = 5;
    private Vector3 _currentVelocity = Vector3.zero;

    private bool _following = false;
    #endregion

    #region Public Methods

    public void Awake()
    {
        _transform = transform;
    }
    public void Initialize(Transform targetTransform, float smoothTime)
    {
        _smoothTime = smoothTime;
        _targetTransform = targetTransform;      
        _following = true;
    }

    public void DontFollowPlayer()
    {
        _following = false;
    }

    #endregion

    #region Unity callbacks

    private void LateUpdate()
    {
        if (!_targetTransform)
            return;

        if (!_following)
            return;

        var distanceX = _targetTransform.position.x - transform.position.x;
        var deltaX = 0.0f;

        if (distanceX > 1.5f)
        {
            deltaX = distanceX - 1.5f;
        }
        if (distanceX < -1.5f && distanceX < 0)
        {
            deltaX = distanceX + 1.5f;
        }
        Vector3 targetPosition = new Vector3(transform.position.x + deltaX, _targetTransform.position.y, _targetTransform.position.z) + _offset;
        

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, _smoothTime);
    }

    #endregion
}