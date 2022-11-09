using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
[ExecuteInEditMode]
public class Detector : MonoBehaviour
{
    public event Action<PlayerUnit> OnChangeTarget;

    private List<PlayerUnit> targets = new List<PlayerUnit>();

    private PlayerUnit target = null;
    public PlayerUnit Target => target;

    [SerializeField] private Transform visualRadius;

    private SphereCollider collider;

    private void Start()
    {
        collider = GetComponent<SphereCollider>();
        PlayerUnit.OnDestroyPlayerUnit += OnDestroyPlayerUnit;
    }

    private void OnDestroy()
    {
        PlayerUnit.OnDestroyPlayerUnit += OnDestroyPlayerUnit;
    }

    private void OnDestroyPlayerUnit(PlayerUnit playerUnit)
    {
        targets.Remove(playerUnit);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerUnit>(out PlayerUnit unit))
        {
            targets.Add(unit);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerUnit>(out PlayerUnit unit))
        {
            targets.Remove(unit);
        }
    }

    private bool IsVisble(PlayerUnit playerUnit)
    {
        RaycastHit hit;
        bool isVisible = Physics.Raycast(transform.position, playerUnit.transform.position - transform.position, out hit) 
            && hit.transform.GetComponentInParent<PlayerUnit>() == playerUnit;

        return isVisible;
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        PlayerUnit newTarget = null;

        if (targets.Count > 0)
        {
            newTarget = targets
                .Where(unit => IsVisble(unit))
                .OrderBy(unit => (pos - unit.transform.position).sqrMagnitude)
                .FirstOrDefault();
        }

        if (target != newTarget)
        {
            target = newTarget;
            OnChangeTarget.SafetyInvoke(target);
        }
    }

    private void Update()
    {
        if (visualRadius)
        {
            float scale = collider.radius * 2 * transform.lossyScale.GetMax();
            Vector3 lossyScale = visualRadius.lossyScale;
            if (!Mathf.Approximately(lossyScale.x, scale))
            {
                visualRadius.SetupLossyScale(new Vector3(scale, 0.1f, scale));
            }
        }
    }
}
