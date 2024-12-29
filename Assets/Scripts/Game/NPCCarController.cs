using UnityEngine;

namespace Assets.Scripts.Game
{
    public class NPCCarController : ABaseCarController, IPoolable
    {
        public GameObject partsOtherWheels;
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
