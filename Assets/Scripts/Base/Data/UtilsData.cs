

using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class UtilsData
{

    public static SubInfo GetSubInfo(JSONObject json)
    {

        SubInfo subInfo = new SubInfo();
        subInfo.assembly = Assembly.Load(json["assemblyName"]);
        subInfo.type = subInfo.assembly.GetType(json["type"]);

        string typeInfoString = json["typeInfo"];

        if (!string.IsNullOrEmpty(typeInfoString))
        {
            Type typeInfo = subInfo.assembly.GetType(typeInfoString);
            if (typeInfo == null)
            {
                Debug.Log($"{typeInfoString} not found!");
            }
            else
            {
                subInfo.data = JsonUtility.FromJson(json["data"].ToString(), typeInfo);
            }
        }

        return subInfo;
    }

    public static SubInfo[] GetSubInfos(JSONArray jsonArray)
    {
        SubInfo[] subInfos = new SubInfo[jsonArray.Count];
        for (int i = 0; i < jsonArray.Count; i++)
        {
            subInfos[i] = GetSubInfo(jsonArray[i].AsObject);
        }
        return subInfos;
    }

    public static object GetType(SubInfo subInfo, GameObject gameObject = null, Action<object> setDataCallback = null)
    {
        object type;
        if (subInfo.type.IsSubclassOf(typeof(Component)))
            type = gameObject.AddComponent(subInfo.type);
        else
        {
            type = Activator.CreateInstance(subInfo.type);
        }

        ISetInfo setInfo = type as ISetInfo;
        if (subInfo.data != null && setInfo != null)
        {
            setInfo.SetInfo(subInfo.data);
        }
        if (setDataCallback != null)
        {
            setDataCallback(type);
        }
        return type;
    }


    public static void AddTypes<T>(SubInfo[] subInfos, List<T> types, GameObject gameObject = null, Action<object> addSubInfoFinishCallback = null) where T : class
    {

        foreach (var type in types)
        {
            var component = type as Component;
            if (component)
            {
                GameObject.DestroyImmediate(component);
            }
        }

        types.Clear();

        if (subInfos == null) return;

        foreach (var subInfo in subInfos)
        {
            object type = GetType(subInfo, gameObject, addSubInfoFinishCallback);
            var typeT = type as T;
            if (typeT != null)
            {
                types.Add(typeT);
            }
        }
    }

}
