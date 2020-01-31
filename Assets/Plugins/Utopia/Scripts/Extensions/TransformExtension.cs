using System.Collections.Generic;
using UnityEngine;

public static partial class ExtensionMethods
{
    public static void SetPositionX(this Transform t, float x)
    {
        var position = t.position;
        t.position = new Vector3(x, position.y, position.z);
    }
    public static void SetPositionY(this Transform t, float y)
    {
        var position = t.position;
        t.position = new Vector3(position.x, y, position.z);
    }
    public static void SetPositionZ(this Transform t, float z)
    {
        var position = t.position;
        t.position = new Vector3(position.x, position.y, z);
    }

    public static void SetLocalPositionX(this Transform t, float x)
    {
        var position = t.localPosition;
        t.localPosition = new Vector3(x, position.y, position.z);
    }
    public static void SetLocalPositionY(this Transform t, float y)
    {
        var position = t.localPosition;
        t.localPosition = new Vector3(position.x, y, position.z);
    }
    public static void SetLocalPositionZ(this Transform t, float z)
    {
        var position = t.localPosition;
        t.localPosition = new Vector3(position.x, position.y, z);
    }

    public static void SetLocalRotationX(this Transform t, float x)
    {
        var rotation = t.localRotation;
        t.localRotation = Quaternion.Euler(x, rotation.y, rotation.z);
    }
    public static void SetLocalRotationY(this Transform t, float y)
    {
        var rotation = t.localRotation;
        t.localRotation = Quaternion.Euler(rotation.x, y, rotation.z);
    }
    public static void SetLocalRotationZ(this Transform t, float z)
    {
        var rotation = t.localRotation;
        t.localRotation = Quaternion.Euler(rotation.x, rotation.y, z);
    }

    public static void SetLocalScaleXY(this GameObject obj, float scale)
    {
        var z = obj.transform.localScale.z;
        obj.transform.localScale = new Vector3(scale, scale, z);
    }

    public static void SetLocalScaleX(this Transform t, float x)
    {
        var scale = t.localScale;
        t.localScale = new Vector3(x, scale.y, scale.z);
    }
    public static void SetLocalScaleY(this Transform t, float y)
    {
        var scale = t.localScale;
        t.localScale = new Vector3(scale.x, y, scale.z);
    }
    public static void SetLocalScaleZ(this Transform t, float z)
    {
        var scale = t.localScale;
        t.localScale = new Vector3(scale.x, scale.y, z);
    }

    public static void ChangeLayer(this GameObject obj, GameObject parent)
    {
        foreach (Transform child in obj.GetComponentsInChildren<Transform>())
        {
            child.gameObject.layer = parent.layer;
        }
    }

    public static void ChangeLayer(this GameObject obj, int layer)
    {
        foreach (Transform child in obj.GetComponentsInChildren<Transform>())
        {
            child.gameObject.layer = layer;
        }
    }

    public static void ChangeLayer(this GameObject obj, int layer, bool includeInactiveChild)
    {
        foreach (Transform child in obj.GetComponentsInChildren<Transform>(includeInactiveChild))
        {
            child.gameObject.layer = layer;
        }
    }

    public static void DestroyAllChildren(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            if (child != null)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    public static List<T> GetComponentsInFirstDepth<T>(this Transform transform)
    {
        var children = new List<T>();

        foreach (Transform child in transform)
        {
            children.Add(child.GetComponent<T>());
        }

        return children;
    }
}
