using UnityEngine;


public static class BaseUtils
{

    public static T GetComponentRef<T>(this Component component, ref T comp)
    {
        comp = component.GetComponent<T>();
        return comp;
    }
    public static T GetComponentInChildrenRef<T>(this Component component, ref T comp)
    {
        comp = component.GetComponentInChildren<T>();
        return comp;
    }
    public static void ValidateCheckNullValue(object objToCheck, string filedName, string component, string objName = "")
    {
        if (objToCheck == null)
        {
            Debug.Log($"{filedName} is null in {component}({objName})");
        }
    }

    public static Sprite LoadSprite(string[] spritePath)
    {
        if (spritePath.Length == 0) return null;
        if (spritePath.Length == 1)
            return Resources.Load<Sprite>(spritePath[0]);
        var sprites = Resources.LoadAll<Sprite>(spritePath[0]);
        foreach (var sprite in sprites)
        {
            if (sprite.name == spritePath[1]) return sprite;
        }

        return null;
    }
}