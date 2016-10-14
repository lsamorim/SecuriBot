using UnityEngine;
using System.Collections;

public static class LayerMaskExtension
{
    public static void SetLayer(GameObject gameObject, LayerMask routinePrefabLayer)
    {
        if (gameObject == null)
            return;

        gameObject.layer = Mathf.Max(0, (int)Mathf.Log(routinePrefabLayer.value, 2));
    }
}