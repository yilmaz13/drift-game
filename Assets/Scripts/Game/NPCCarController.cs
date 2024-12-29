using UnityEngine;

namespace Assets.Scripts.Game
{
    public class NPCCarController : MonoBehaviour, IPoolable
    {
        public GameObject partsOtherWheels;
        public GameObject partsSteeringWheel;
        public GameObject partsAllCar;
        public GameObject[] wheels;
        public GameObject[] rims;
        public GameObject rim0;
        public GameObject rim1;
        public Transform[] rutTransforms;

        // Start is called before the first frame update

        public void Initialize()
        {

        }

        public void OnPoolSpawn()
        {

        }

        public void OnReturnPool()
        {

        }
    }
}
