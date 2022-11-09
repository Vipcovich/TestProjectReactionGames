using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PathMover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Path path;

    private GameObject target;
    private Coroutine walkPathCoroutine;

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    private IEnumerator WalkByPath()
    {
        bool forward = true;
        Transform point = null;

        while (true)
        {
            if (!path || path.Points.Length == 0 || !agent || target)
            {
                yield return null;
                continue;
            }

            if (!point || agent.remainingDistance <= 1f)
            {
                point = path.GetNextPoint(point, ref forward);
                agent.destination = point.position;
            }
            yield return null;
        }
    }

    private void Update()
    {
        if (target)
        {
            transform.LookAt(target.transform);
            MoveTo(target.transform.position);
        }

        if (walkPathCoroutine == null)
        {
            walkPathCoroutine = StartCoroutine(WalkByPath());
        }
    }

    private void MoveTo(Vector3 moveTo)
    {
        if (walkPathCoroutine != null)
        {
            StopCoroutine(walkPathCoroutine);
            walkPathCoroutine = null;
        }

        if (agent)
        {
            agent.destination = moveTo;
        }
    }
}
