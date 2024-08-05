using SimpleJSON;
using UnityEngine;

public class StaticData : BaseData
{
    public override void LoadData(string path)
    {
        var textAssets = Resources.LoadAll<TextAsset>(path);
        LoadData(textAssets);
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
