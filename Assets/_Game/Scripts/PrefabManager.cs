using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public ObjectType tag;
        public GameObject objectPrefab;
        public int size;
    }
    public List<Pool> Pools;
    public Dictionary<ObjectType, Queue<GameObject>> poolDictionary = new Dictionary<ObjectType, Queue<GameObject>>();

    public static PrefabManager Instance { get; private set; }
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

    void Start()
    {

        foreach (Pool pool in Pools)
        {
            Queue<GameObject> objQueue = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.objectPrefab);
                obj.SetActive(false);
                objQueue.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objQueue);
        }
    }

    public GameObject PopFromPool(ObjectType tag, Vector3 spawnPos, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("pool not exist " + tag);
            return null;
        }

        GameObject obj = poolDictionary[tag].Dequeue();
        Transform objTrans = obj.transform;

        obj.SetActive(true);
        objTrans.position = spawnPos;
        objTrans.rotation = rotation;

        IPooledObject pooledObject = obj.GetComponent<IPooledObject>();

        if (pooledObject != null)
        {
            pooledObject.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(obj);

        return obj;
    }

    public enum ObjectType
    {
        DefaultBlock,
        PlayerBlock,
        WallBlock,
        StackBlock,
        BridgeBlockVertical,
        BridgeBlockHorizontal,
        CornerBlock,
        WideBrigeBlockVertical,
        WideBrigeBlockHorizontal,
        BounceBlock,
        FinishLine,
        ChestBlock
    }
}
