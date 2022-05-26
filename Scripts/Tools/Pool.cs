using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public GameObject Prefab => prefab;

    [SerializeField] private GameObject prefab;
    [SerializeField] private int size = 1;
    private Queue<GameObject> _queue;

    private Transform _parent;
    public void Initialize(Transform parent)
    {
        _queue = new Queue<GameObject>();
        this._parent = parent;

        for (var i = 0; i < size; i++)
        {
            _queue.Enqueue(Copy());
        }
    }
    
    private GameObject Copy()
    {
        var copy = Object.Instantiate(prefab, _parent);
        copy.SetActive(false);
        return copy;
    }

    private GameObject AvailableObject()
    {
        GameObject availableObject = null;

        if (_queue.Count>0 && !_queue.Peek().activeSelf)
        {
            availableObject = _queue.Dequeue();
        }
        else
        {
            availableObject = Copy();
        }
        
        _queue.Enqueue(availableObject);

        return availableObject;
    }

    public GameObject PreparedObject()
    {
        var preparedObject = AvailableObject();

        preparedObject.SetActive(true);
        
        return preparedObject;
    }
    
    public GameObject PreparedObject(Vector3 position)
    {
        var preparedObject = AvailableObject();

        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        
        return preparedObject;
    }
    
    public GameObject PreparedObject(Vector3 position, Quaternion rotation)
    {
        var preparedObject = AvailableObject();

        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;
        
        return preparedObject;
    }
    
    public GameObject PreparedObject(Vector3 position, Quaternion rotation, Vector3 localScale)
    {
        var preparedObject = AvailableObject();

        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;
        preparedObject.transform.localScale = localScale;
        
        return preparedObject;
    }
}
