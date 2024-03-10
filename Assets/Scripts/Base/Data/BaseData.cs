using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;

public class BaseData
{
    protected Dictionary<string, JSONNode> data_dict = new Dictionary<string, JSONNode>();
    public virtual void LoadData(string path)
    {
        var textAssets = Resources.LoadAll<TextAsset>(path);
        LoadData(textAssets);
    }

    public T GetData<T>(int level)
    {
        var data = JsonUtility.FromJson<T>(data_dict["Level" + level].AsObject.ToString());
        return data;
    }

    public T GetData<T>(string key)
    {
        var data = JsonUtility.FromJson<T>(data_dict[key].AsObject.ToString());
        return data;
    }



    public virtual void LoadData(TextAsset[] textAssets)
    {
        foreach (var textAsset in textAssets)
        {
            LoadData(textAsset);
        }
    }

    public virtual void LoadData(TextAsset textAsset)
    {
        data_dict.Add(textAsset.name, JSON.Parse(textAsset.text));
    }

    public virtual void LoadData(string name, TextAsset textAsset)
    {
        data_dict.Add(name, JSON.Parse(textAsset.text));
    }

}
