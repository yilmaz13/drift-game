using UnityEngine;

namespace Assets.Scripts.Game
{
    public interface IDriftGameListener
    {
        void FollowCameraPlayer(Transform transform);
    }
}
