using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DeathVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other?.gameObject?.GetComponentInChildren<Unit>()?.TakeDamage(float.MaxValue);
    }
}
