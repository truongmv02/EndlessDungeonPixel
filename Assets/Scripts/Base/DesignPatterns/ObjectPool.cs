

using System.Collections.Generic;
using UnityEngine;
public class ObjectPool : Singleton<ObjectPool>
{
    private Dictionary<string, List<GameObject>> pools = new Dictionary<string, List<GameObject>>();

    public void CreatePool(GameObject prefab, int size = 5)
    {

        for (int i = 0; i < size; i++)
        {
            GameObject obj = CreateObject(prefab);
            obj.SetActive(false);
        }
    }

    public GameObject CreateObject(GameObject prefab)
    {
        Transform parent;
        if (!pools.ContainsKey(prefab.name))
        {
            List<GameObject> list = new List<GameObject>();
            pools[prefab.name] = list;
            parent = new GameObject(prefab.name + "Holder").transform;
        }
        else
        {
            parent = pools[prefab.name][0].transform.parent;
        }

        GameObject obj = GameObject.Instantiate(prefab, parent);
        obj.name = prefab.name;
        pools[prefab.name].Add(obj);
        return obj;
    }

    public T GetObject<T>(GameObject prefab) where T : MonoBehaviour
    {
        GameObject obj = GetObject(prefab);
        return obj.GetComponent<T>();
    }

    public GameObject GetObject(GameObject prefab)
    {
        GameObject obj = null;
        if (pools.ContainsKey(prefab.name))
        {
            var list = pools[prefab.name];
            foreach (var item in list)
            {
                if (!item.activeSelf)
                {
                    obj = item;
                    break;
                }
            }
        }
        if (obj == null)
        {
            obj = CreateObject(prefab);
        }
        obj.SetActive(true);
        obj.GetComponent<IResetObject>()?.ResetObject();
        return obj;
    }

    public void DestroyObject(GameObject prefab, float delayTime = 0f)
    {
        LeanTween.delayedCall(delayTime, () =>
        {
            if (!pools.ContainsKey(prefab.name))
            {
                GameObject.Destroy(prefab);
                return;
            }
            prefab.SetActive(false);
        });

    }

}
