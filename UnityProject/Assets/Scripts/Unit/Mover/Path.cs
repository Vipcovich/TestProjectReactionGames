using UnityEngine;
using System;

[ExecuteInEditMode]
public class Path : MonoBehaviour
{
    [SerializeField] private bool cyclic;
    public bool Cyclic => cyclic;

    [SerializeField] Transform[] points = new Transform[0];
    public Transform[] Points => points;

    public Transform GetNextPoint(Transform currentPoint, ref bool forward)
    {
        if (points == null || points.Length == 0)
        {
            return null;
        }

        if (!currentPoint)
        {
            return points[0];
        }

        int idx = Array.FindIndex(points, obj => obj == currentPoint);
        if (idx < 0)
        {
            return points[0];
        }

        int nextIdx = idx + (forward ? 1 : -1);
        if (nextIdx < 0 || nextIdx >= points.Length)
        {
            if (cyclic)
            {
                return nextIdx < 0 ? points[points.Length - 1] : points[0];
            }
            else
            {
                forward = !forward;
                return points[idx + (forward ? 1 : -1)];
            }
        }

        return points[nextIdx];
    }


    private void OnDrawGizmos()
    {
        int childCount = transform.childCount;
        if (points.Length != childCount)
        {
            points = new Transform[childCount];
            for (int i = 0; i < childCount; i++)
            {
                points[i] = transform.GetChild(i);
            }
        }

        Gizmos.color = Color.red;

        for (int i = 0; i < childCount - 1; i++)
        {
            Gizmos.DrawLine(points[i].position, points[i + 1].position);
        }

        if (cyclic && childCount > 2)
        {
            Gizmos.DrawLine(points[0].position, points[childCount - 1].position);
        }
    }
}
