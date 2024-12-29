using PoolSystem;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }
    //  METHODS

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetGameObject(string strg)
    {
        var obj = ObjectPooler.Instance.Spawn(strg, new Vector3());
        return obj;
    }
    public GameObject GetParticles(Vector3 pos, string spawnName)
    {
        var fx = ObjectPooler.Instance.Spawn(spawnName, new Vector3());
        fx.transform.position = pos;
        return fx;
    }
}
