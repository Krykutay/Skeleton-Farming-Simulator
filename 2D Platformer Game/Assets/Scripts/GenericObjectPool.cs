using System.Collections.Generic;
using UnityEngine;

public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component
{

    [SerializeField] T _prefab;

    [SerializeField] int _initialPoolSize;

    Queue<T> _objectsQueue = new Queue<T>();

    public static GenericObjectPool<T> Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        AddObjects(_initialPoolSize);
    }

    public T Get()
    {
        if (_objectsQueue.Count == 0)
        {
            AddObjects(1);
        }

        T objectToGet = _objectsQueue.Dequeue();

        objectToGet.gameObject.SetActive(true);

        return objectToGet;
    }

    public T Get(Vector3 position, Vector3 rotation)
    {
        if (_objectsQueue.Count == 0)
        {
            AddObjects(1);
        }

        T objectToGet = _objectsQueue.Dequeue();

        objectToGet.transform.position = position;
        objectToGet.transform.rotation = Quaternion.Euler(rotation);
        objectToGet.gameObject.SetActive(true);

        return objectToGet;
    }

    public void AddObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            T newObject = Instantiate(_prefab);
            newObject.gameObject.SetActive(false);
            _objectsQueue.Enqueue(newObject);
        }
    }

    public void ReturnToPool(T objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        _objectsQueue.Enqueue(objectToReturn);
    }

}
