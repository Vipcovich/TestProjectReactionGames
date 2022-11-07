using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class PathMover : MonoBehaviour
{
    [SerializeField] private List<Transform> path = new List<Transform>();

    private GameObject target;

    public List<Transform> Path
    {
        get { return path; }
        set
        {
            if (path != value)
            {
                path = value;
                SetTarget(path?.First().gameObject);
            }
        }
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    private void Update()
    {
        if (target)
        {
            transform.LookAt(target.transform);
        }
    }
}
