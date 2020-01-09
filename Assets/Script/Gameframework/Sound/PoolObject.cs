using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject
{
    private GameObject _prefab;
    private int _maxQuantity;
    private static Stack<GameObject> _available;
    private static Stack<GameObject> _inUse;

    public PoolObject(GameObject prefab, int maxQuantity = -1)
    {
        _prefab = prefab;
        _maxQuantity = maxQuantity;
        if (_maxQuantity != -1)
        {
            _available = new Stack<GameObject>(_maxQuantity);
            _inUse = new Stack<GameObject>(_maxQuantity);
        }
        else {
            _available = new Stack<GameObject>();
        }
    }

    public GameObject GetNewObject() {
        lock (_available)
        {
            if (_available.Count != 0)
            {
                GameObject gameObject = _available.Pop();
                return gameObject;
            }
            else
            {
                GameObject gameObject = GameObject.Instantiate(_prefab);
                return gameObject;
            }
        }
    }

    public void ReleaseObject(GameObject gameObject)
    {
        CleanUp(gameObject);

        lock (_available)
        {
            _available.Push(gameObject);
        }
    }

    private void CleanUp(GameObject gameObject)
    {
        gameObject = null;
    }
}
