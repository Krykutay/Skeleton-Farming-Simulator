using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        [SerializeField] string _tag;
        [SerializeField] GameObject _prefab;
        [SerializeField] int _initialSize;

        public string tag { get { return _tag; } }
        public GameObject prefab { get { return _prefab; } }
        public int initialSize { get { return _initialSize; } }
    }

    [SerializeField] List<Pool> _pools;
    Dictionary<string, Queue<GameObject>> poolsDict;
    Dictionary<string, GameObject> prefabsDict;

    public static ObjectPoolingManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        poolsDict = new Dictionary<string, Queue<GameObject>>();
        prefabsDict = new Dictionary<string, GameObject>();

        foreach (Pool pool in _pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            poolsDict.Add(pool.tag, objectPool);
            prefabsDict.Add(pool.tag, pool.prefab);

            AddObjects(pool.tag, pool.prefab, pool.initialSize);
        }
    }

    public GameObject Get(string tag)
    {
        if (poolsDict[tag].Count == 0)
        {
            AddObjects(tag, prefabsDict[tag], 1);
        }

        return poolsDict[tag].Dequeue();
    }

    void AddObjects(string tag, GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newObject = Instantiate(prefab);
            newObject.SetActive(false);
            poolsDict[tag].Enqueue(newObject);
        }
    }

    public void ReturnToPool(string tag, GameObject objectToReturn)
    {
        objectToReturn.SetActive(false);
        poolsDict[tag].Enqueue(objectToReturn);
    }

}
