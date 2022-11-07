using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class ExitZone : MonoBehaviour
{
    public static event Action<PlayerUnit> OnPlayerEnterToZone;
    public static event Action<PlayerUnit> OnPlayerExitFromZone;

    private void OnTriggerEnter(Collider other)
    {
        PlayerUnit playerUnit = other.gameObject?.GetComponentInParent<PlayerUnit>();
        if (playerUnit)
        {
            OnPlayerEnterToZone.SafetyInvoke(playerUnit);
            playerUnit.gameObject.layer = PlayerUnit.DefaultLayer;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerUnit playerUnit = other.gameObject?.GetComponentInParent<PlayerUnit>();
        if (playerUnit)
        {
            OnPlayerExitFromZone.SafetyInvoke(playerUnit);
            playerUnit.gameObject.layer = PlayerUnit.PlayerLayer;
        }
    }
}
