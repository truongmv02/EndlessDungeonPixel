using SimpleJSON;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseData
{
    protected Dictionary<string, JSONNode> data_dict = new Dictionary<string, JSONNode>();
    public abstract void LoadData(string path);

    public T GetData<T>(int level)
    {
        string key = "Level" + level;
        if (!data_dict.ContainsKey(key))
        {
            Debug.Log($"{key} not found");
            return default;
        }
        var data = JsonUtility.FromJson<T>(data_dict[key].AsObject.ToString());
        return data;
    }

    public JSONNode GetData(int level)
    {
        string key = "Level" + level;
        return GetData(key);
    }

    public T GetData<T>(string key)
    {
        var jsonObj = GetData(key);
        if (jsonObj == null) return default;
        return GetData<T>(jsonObj.AsObject);
    }

    public T GetData<T>(JSONObject jsonObj)
    {
        if (jsonObj == null) return default;
        return JsonUtility.FromJson<T>(jsonObj.AsObject.ToString());
    }

    protected List<T> GetAllData<T>(Action<T, JSONObject> callback = null)
    {
        List<T> result = new();
        foreach (KeyValuePair<string, JSONNode> data in data_dict)
        {
            var jsonObj = data.Value.AsObject;
            T value = GetData<T>(jsonObj);
            result.Add(value);
            if (callback != null)
            {
                callback(value, jsonObj);
            }

        }

        return result;
    }


    public JSONNode GetData(string key)
    {
        if (!data_dict.ContainsKey(key))
        {
            Debug.Log($"{key} not found");
            return default;
        }
        return data_dict[key];
    }

}
