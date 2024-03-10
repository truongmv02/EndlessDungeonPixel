
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseMultiData
{
    public Dictionary<string, BaseData> Datas { get; protected set; } = new Dictionary<string, BaseData>();

    public bool ContainsKey(string key)
    {
        return Datas.ContainsKey(key);
    }

    public virtual void LoadData<T>(string path) where T : BaseData, new()
    {
        var textAssets = Resources.LoadAll<TextAsset>(path);
        foreach (var textAsset in textAssets)
        {
            string[] fileName = textAsset.name.ToString().Split('.');

            if (ContainsKey(fileName[0]))
            {
                Datas[fileName[0]].LoadData(fileName[1], textAsset);
            }
            else
            {
                T data = new T();
                data.LoadData(fileName[1], textAsset);
                Datas.Add(fileName[0], data);
            }
        }
    }
}
