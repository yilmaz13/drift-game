using Assets.Scripts.PoolSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class RoadController : MonoBehaviour
    {
        #region Fields
        [SerializeField] private float segmentLength = 120f;
        [SerializeField] private GameObject roadSegmentPrefab;
        [SerializeField] private int initialSegments = 5;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float safeZone = 360;
        private Queue<GameObject> roadSegments = new Queue<GameObject>();
        private float spawnZ = 0f;

        private Transform _roadParent;
        private SpawnManager _spawnManager;
        public float SegmentLength => segmentLength;
        #endregion

        #region Unity Methods
        void Update()
        {
            if (playerTransform == null)
            {
                return;
            }

            if (playerTransform.position.z - safeZone > (spawnZ - initialSegments * segmentLength))
            {
                SpawnSegment();
                RemoveOldSegment();
            }
        }
       
        #endregion

        #region Public Methods
        public void Initialize()
        {
            _roadParent = transform;
            _spawnManager = SpawnManager.Instance;

            for (int i = 0; i < initialSegments; i++)
            {
                SpawnSegment();
            }
            SubscribeEvents();
        }

        public void SetPlayerTransform(Transform playerTransform)
        {
            this.playerTransform = playerTransform;
        }

        public void Unload()
        {
            RemoveAllSegment();
            UnsubscribeEvents();
            spawnZ = 0;
        }
        #endregion

        #region Private Methods
        private void SpawnSegment()
        {
            GameObject segment = _spawnManager.GetGameObject("Road");

            segment.transform.SetParent(transform);
            segment.transform.position = Vector3.forward * spawnZ;
            roadSegments.Enqueue(segment);
            spawnZ += segmentLength;
        }

        private void RemoveOldSegment()
        {
            if (roadSegments.Count == 0)
            {
                return;
            }

            GameObject oldSegment = roadSegments.Dequeue();
            oldSegment.GetComponent<PoolObject>().GoToPool();
        }

        private void RemoveAllSegment()
        {
            for (int i = roadSegments.Count; i > 0; i--)
            {
                roadSegments.Dequeue().GetComponent<PoolObject>().GoToPool();
            }
        }

        private void SubscribeEvents()
        {
            GameEvents.OnSpawnedPlayer += SetPlayerTransform;
        }

        private void UnsubscribeEvents()
        {
            GameEvents.OnSpawnedPlayer -= SetPlayerTransform;
        }
        #endregion

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }
    }
}
