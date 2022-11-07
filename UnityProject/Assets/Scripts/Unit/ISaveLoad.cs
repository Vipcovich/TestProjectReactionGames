using System;
using UnityEngine;

public interface ISaveLoad
{
    GameObject gameObject { get; }
    Type GetType();
    SaveLoadObject Save();
    void Load(SaveLoadObject saveLoadObject);
}

[Serializable]
public class SaveLoadObject
{
    public string Path;
    public string Data;

    public static SaveLoadObject Save<TComp, TKeeper>(TComp comp, TKeeper keeper) where TComp : Component
    {
        SaveLoadObject obj = new SaveLoadObject();
        obj.Path = comp.GetPath();
        obj.Data = JsonUtility.ToJson(keeper);
        return obj;
    }

    public TKeeper Load<TKeeper>()
    {
        return JsonUtility.FromJson<TKeeper>(Data);
    }
}

[Serializable]
public class SaveTransform
{
    public float[] Position = new float[3];
    public float[] Rotation = new float[4];
    public float[] Scale = new float[3];

    public SaveTransform(Transform transform)
    {
        Position[0] = transform.localPosition.x;
        Position[1] = transform.localPosition.y;
        Position[2] = transform.localPosition.z;

        Rotation[0] = transform.localRotation.w;
        Rotation[1] = transform.localRotation.x;
        Rotation[2] = transform.localRotation.y;
        Rotation[3] = transform.localRotation.z;

        Scale[0] = transform.localScale.x;
        Scale[1] = transform.localScale.y;
        Scale[2] = transform.localScale.z;
    }

    public void Deserial(Transform transform)
    {
        transform.localPosition = new Vector3(Position[0], Position[1], Position[2]);
        transform.localRotation = new Quaternion(Rotation[1], Rotation[2], Rotation[3], Rotation[0]);
        transform.localScale = new Vector3(Scale[0], Scale[1], Scale[2]);
    }
}



//public interface ISaveLoad
//{
//    GameObject gameObject { get; }
//    Type GetType();
//    SaveLoadObject Save();
//    void Load(SaveLoadObject saveLoadObject);
//}

//[Serializable]
//public class SaveLoadObject
//{
//    public string Path;

//    public SaveLoadObject(Component comp, string compName)
//    {
//        Path = string.Format("{0}/{1}", comp.gameObject.GetPath(), compName);
//    }

//    public void Deserial(Component comp)
//    {
//    }
//}

//[Serializable]
//public class SaveTransform : SaveLoadObject
//{
//    public float[] Position = new float[3];
//    public float[] Rotation = new float[4];
//    public float[] Scale = new float[3];

//    public SaveTransform(Transform transform, string compName) : base (transform, compName)
//    {
//        Position[0] = transform.localPosition.x;
//        Position[1] = transform.localPosition.y;
//        Position[2] = transform.localPosition.z;

//        Rotation[0] = transform.localRotation.w;
//        Rotation[1] = transform.localRotation.x;
//        Rotation[2] = transform.localRotation.y;
//        Rotation[3] = transform.localRotation.z;

//        Scale[0] = transform.localScale.x;
//        Scale[1] = transform.localScale.y;
//        Scale[2] = transform.localScale.z;
//    }

//    public void Deserial(Transform transform)
//    {
//        transform.localPosition = new Vector3(Position[0], Position[1], Position[2]);
//        transform.localRotation = new Quaternion(Rotation[1], Rotation[2], Rotation[3], Rotation[0]);
//        transform.localScale = new Vector3(Scale[0], Scale[1], Scale[2]);
//    }
//}
