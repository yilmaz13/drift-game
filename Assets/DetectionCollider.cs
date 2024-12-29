using UnityEngine;

public class DetectionCollider : MonoBehaviour
{
    public delegate void Action(GameObject collidedObject);
    public event Action OnCollisionDetected;

    private void OnTriggerEnter(Collider other)
    {
        OnCollisionDetected?.Invoke(other.gameObject);
    }
}
