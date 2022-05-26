using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private Pool[] playerProjectilePools;

    private static Dictionary<GameObject, Pool> _dictionary;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _dictionary = new Dictionary<GameObject, Pool>();
        Initialize(playerProjectilePools);
    }

    private void Initialize(Pool[] pools)
    {
        foreach (var pool in pools)
        {
        #if UNITY_EDITOR
            if (_dictionary.ContainsKey(pool.Prefab))
            {
                Debug.LogError("在多个对象池中发现了相同的预制体。 预制体："+pool.Prefab.name);
                continue;
            }
        #endif
            
            _dictionary.Add(pool.Prefab, pool);
            
            var poolParent = new GameObject("Pool:" + pool.Prefab.name).transform;

            poolParent.parent = transform;

            pool.Initialize(poolParent);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="prefab">指定游戏对象预制体</param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab)
    {
#if UNITY_EDITOR
        if (!_dictionary.ContainsKey(prefab))
        {
            Debug.LogError("池管理器找不到这个预制体。 预制体："+prefab.name);
            
            return null;
        }
#endif
        return _dictionary[prefab].PreparedObject();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="prefab">指定游戏对象预制体</param>
    /// <param name="position">指定释放位置</param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position)
    {
#if UNITY_EDITOR
        if (!_dictionary.ContainsKey(prefab))
        {
            Debug.LogError("池管理器找不到这个预制体。 预制体："+prefab.name);
            
            return null;
        }
#endif
        return _dictionary[prefab].PreparedObject(position);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="prefab">指定游戏对象预制体</param>
    /// <param name="position">指定释放位置</param>
    /// <param name="rotation">指定的旋转值</param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation)
    {
#if UNITY_EDITOR
        if (!_dictionary.ContainsKey(prefab))
        {
            Debug.LogError("池管理器找不到这个预制体。 预制体："+prefab.name);
            
            return null;
        }
#endif
        return _dictionary[prefab].PreparedObject(position, rotation);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="prefab">指定游戏对象预制体</param>
    /// <param name="position">指定释放位置</param>
    /// <param name="rotation">指定的旋转值</param>
    /// <param name="localScale">指定的缩放值</param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 localScale)
    {
#if UNITY_EDITOR
        if (!_dictionary.ContainsKey(prefab))
        {
            Debug.LogError("池管理器找不到这个预制体。 预制体："+prefab.name);
            
            return null;
        }
#endif
        return _dictionary[prefab].PreparedObject(position, rotation, localScale);
    }
}
