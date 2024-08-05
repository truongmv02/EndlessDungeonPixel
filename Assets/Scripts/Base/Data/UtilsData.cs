using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class UtilsData
{
    public static Sprite GetSprite(string path)
    {
        string[] paths = path.Split('#');

        return BaseUtils.LoadSprite(paths);
    }
    public static Sprite GetSprite(JSONObject json)
    {
        if (json["sprite"] != null)
        {
            var spriteArray = json["sprite"].AsArray;
            var sprites = new string[spriteArray.Count];
            for (int i = 0; i < spriteArray.Count; i++)
            {
                sprites[i] = spriteArray[i];
            }
            return BaseUtils.LoadSprite(sprites);
        }
        return null;
    }
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

    private static object GetType(SubInfo subInfo, GameObject gameObject = null, Action<object> setDataCallback = null)
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

    private static void ClearTypes<T>(List<T> types) where T : class
    {
        foreach (var type in types)
        {
            var component = type as Component;
            if (component)
            {
                GameObject.Destroy(component);
            }
        }

        types.Clear();
    }

    private static void ClearState<T>(List<T> types)
    {
        foreach (var type in types)
        {
            var component = type as Component;
            if (component)
            {
                GameObject.Destroy(component.gameObject);
                GameObject.Destroy(component);
            }
        }

        types.Clear();
    }
    public static void AddStates<T>(SubInfo[] subInfos, List<T> types, Transform parent, Action<object> addSubInfoFinish = null) where T : class
    {
        var typesAlready = new List<T>(types);
        var currentTypes = new List<T>(types);
        types.Clear();
        foreach (var subInfo in subInfos)
        {
            bool typeExists = false;
            foreach (var type in currentTypes)
            {
                if (subInfo.type == type.GetType())
                {
                    ISetInfo setInfo = type as ISetInfo;
                    if (subInfo.data != null && setInfo != null)
                    {
                        setInfo.SetInfo(subInfo.data);
                    }
                    currentTypes.Remove(type);
                    types.Add(type);
                    addSubInfoFinish?.Invoke(type);
                    typeExists = true;
                    break;
                }
            }
            if (!typeExists)
            {
                var stateInfo = subInfo.data as StateInfo;
                GameObject gameObject = new GameObject(stateInfo.stateName);
                gameObject.transform.SetParent(parent);
                object type = GetType(subInfo, gameObject, addSubInfoFinish);
                var typeT = type as T;
                types.Add(typeT);
            }
        }

        ClearState(currentTypes);
    }
    public static void AddTypes<T>(SubInfo[] subInfos, List<T> types, GameObject gameObject = null, Action<object> addSubInfoFinish = null) where T : class
    {

        if (subInfos == null)
        {
            ClearTypes(types);
            return;
        }
        var typesAlready = new List<T>(types);
        var currentTypes = new List<T>(types);
        var typesAdded = new List<T>();
        types.Clear();
        foreach (var subInfo in subInfos)
        {
            bool typeExists = false;
            foreach (var type in currentTypes)
            {
                if (subInfo.type == type.GetType())
                {
                    ISetInfo setInfo = type as ISetInfo;
                    if (subInfo.data != null && setInfo != null)
                    {
                        setInfo.SetInfo(subInfo.data);
                    }
                    currentTypes.Remove(type);
                    types.Add(type);
                    addSubInfoFinish?.Invoke(type);
                    typeExists = true;
                    break;
                }
            }
            if (!typeExists)
            {
                object type = GetType(subInfo, gameObject, addSubInfoFinish);
                var typeT = type as T;
                types.Add(typeT);
            }
        }

        ClearTypes(currentTypes);
    }

}