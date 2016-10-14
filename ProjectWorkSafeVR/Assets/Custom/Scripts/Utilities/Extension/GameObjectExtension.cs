using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameObjectExtension
{
    public static T GetInterface<T>(this GameObject go) where T : class
    {

        if (!typeof(T).IsInterface)
        {
            Debug.LogError(typeof(T).ToString() + " is not an interface");
            return null;
        }

        return go.GetComponent<T>();
    }

    public static GameObject Near<T>(this List<GameObject> self, T reference) where T : Component
    {
        if (reference == null)
            return self.FirstOrDefault();
        var objReference = reference.gameObject.transform.position;
        float minDistance = float.MaxValue;
        GameObject bestObj = null;
        foreach (var obj in self)
        {
            var dist = Vector3.Distance(objReference, obj.transform.position);
            if (dist < minDistance)
            {
                bestObj = obj;
                minDistance = dist;
            }
        }
        return bestObj;
    }

    public static GameObject Near(this List<GameObject> self, GameObject reference)
    {
        if (reference == null)
            return self.FirstOrDefault();
        var objReference = reference.gameObject.transform.position;
        float minDistance = float.MaxValue;
        GameObject bestObj = null;
        foreach (var obj in self)
        {
            var dist = Vector3.Distance(objReference, obj.transform.position);
            if (dist < minDistance)
            {
                bestObj = obj;
                minDistance = dist;
            }
        }
        return bestObj;
    }
}
