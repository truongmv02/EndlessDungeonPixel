using UnityEngine;


public static class BaseUtils
{
    public static void ValidateCheckNullValue(object objToCheck, string filedName, string component, string objName = "")
    {
        if (objToCheck == null)
        {
            Debug.Log($"{filedName} is null in {component}({objName})");
        }
    }
}