using UnityEngine;

namespace Assets.Scripts.Game
{
    public class NPCCarController : BaseCarController, IPoolable
    {
        public GameObject partsSteeringWheel;  
        public Transform[] rutTransforms;

        public override void Initialize()
        {
            base.Initialize();
            //TODO
        }

        public void OnPoolSpawn()
        {

        }

        public void OnReturnPool()
        {

        }
    }
}
