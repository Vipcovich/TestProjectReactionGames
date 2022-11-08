using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory : SingletonMonoBehaviour<ObjectFactory>
{
    [SerializeField] private List<GameObject> prototypes = new List<GameObject>();

    public T Create<T>() where T:Component
    {
        for (int i = 0; i < prototypes.Count; i++)
        {
            GameObject obj = prototypes[i];
            if (obj.GetComponentInChildren<T>())
            {
                return Instantiate(obj).GetComponentInChildren<T>();
            }
        }

        return null;
    }
}
