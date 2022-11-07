using UnityEngine;

public static class TransformExtensions
{
    public static void SetupLossyScale(this Transform transform, Vector3 lossyScale)
    {
        transform.localScale = transform.parent.InverseTransformVector(lossyScale);
    }
}
