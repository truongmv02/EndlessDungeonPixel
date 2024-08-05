

using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject prefab;
    public int count;
}

public class ObjectPool : SingletonMonoBehaviour<ObjectPool>
{
    private Dictionary<string, List<GameObject>> pools = new Dictionary<string, List<GameObject>>();
    [SerializeField] ObjectPoolItem[] objectPools;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        if (objectPools == null)
        {
            return;
        }
        foreach (var objItem in objectPools)
        {
            for (int i = 0; i < objItem.count; i++)
            {
                CreateObject(objItem.prefab).SetActive(false);
            }
        }
    }

    private GameObject CreateObject(GameObject prefab, object info = null)
    {
        Transform parent;
        if (!pools.ContainsKey(prefab.name))
        {
            List<GameObject> list = new List<GameObject>();
            pools[prefab.name] = list;
            parent = new GameObject(prefab.name + "Holder").transform;
            parent.SetParent(transform);
        }
        else
        {
            parent = pools[prefab.name][0].transform.parent;
        }

        GameObject obj = GameObject.Instantiate(prefab, parent);

        if (info != null)
            obj.GetComponent<ISetInfo>()?.SetInfo(info);

        obj.name = prefab.name;
        pools[prefab.name].Add(obj);
        return obj;
    }
    private GameObject GetObjectInactive(GameObject prefab)
    {
        if (pools.ContainsKey(prefab.name))
        {
            var list = pools[prefab.name];
            foreach (var item in list)
            {
                if (!item.activeSelf)
                {
                    return item;

                }
            }
        }
        return null;
    }
    private void SetObjectValue(GameObject obj, Vector2 position, Vector2 direction)
    {
        obj.transform.up = direction;
        obj.transform.position = position;
        obj.SetActive(true);
        obj.GetComponent<IResetObject>()?.ResetObject();
    }
    public GameObject GetObject(GameObject prefab, Vector2 position, Vector2 direction, object info = null)
    {
        GameObject obj = GetObjectInactive(prefab);
        obj = obj != null ? obj : CreateObject(prefab, info);
        SetObjectValue(obj, position, direction);

        if (info != null)
            obj.GetComponent<ISetInfo>()?.SetInfo(info);
        return obj;
    }

    public void DestroyObject(GameObject prefab, float delayTime = 0f)
    {
        LeanTween.delayedCall(delayTime, () =>
        {
            prefab.SetActive(false);
        });

    }

}
