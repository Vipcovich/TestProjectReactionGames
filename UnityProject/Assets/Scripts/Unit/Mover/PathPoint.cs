using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PathPoint : MonoBehaviour
{
    private void Update()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            int groundLayer = LayerMask.NameToLayer("Ground");

            RaycastHit raycastHit;
            if (Physics.Raycast(transform.position, -transform.up, out raycastHit, groundLayer)
                || Physics.Raycast(transform.position, transform.up, out raycastHit, groundLayer))
            {
                transform.position = raycastHit.point;
            }
        }
#endif
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, Vector3.one);
    }
}
