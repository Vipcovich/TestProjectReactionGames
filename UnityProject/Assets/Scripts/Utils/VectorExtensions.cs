using System;
using UnityEngine;

public static class VectorExtensions
{
    public static float GetMax(this Vector3 vector)
    {
        return Mathf.Max(Mathf.Max(vector.x, vector.y), vector.z);
    }
}
