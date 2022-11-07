using System;
using System.Text;
using UnityEngine;

public static class GameObjectExtensions
{
    private static string sep = "/";

    private static void GetPath(GameObject gameObject, StringBuilder stringBuilder)
    {
        Transform transform = gameObject.transform;
        while (transform)
        {
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Insert(0, sep);
            }
            stringBuilder.Insert(0, transform.name);
            transform = transform.parent;
        }
    }

    public static string GetPath(this GameObject gameObject)
    {
        StringBuilder stringBuilder = new StringBuilder();
        GetPath(gameObject, stringBuilder);
        return stringBuilder.ToString();
    }

    public static string GetPath(this Component component)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Insert(0, component.GetType().Name);
        GetPath(component.gameObject, stringBuilder);
        return stringBuilder.ToString();
    }
}

