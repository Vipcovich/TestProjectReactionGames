using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
    public static int PlayerLayer => LayerMask.NameToLayer("Player");
    public static int DefaultLayer => LayerMask.NameToLayer("Default");

    public static List<PlayerUnit> PlayerUnits { get; } = new List<PlayerUnit>();

    public static event Action<PlayerUnit> OnCreatePlayerUnit;
    public static event Action<PlayerUnit> OnDestroyPlayerUnit;

    [SerializeField] private Transform cameraPosition;

    public string Name => name;

    protected override void Start()
    {
        base.Start();
        PlayerUnits.Add(this);
        OnCreatePlayerUnit.SafetyInvoke(this);
    }

    private void OnDestroy()
    {
        OnDestroyPlayerUnit.SafetyInvoke(this);
        PlayerUnits.Remove(this);
    }

    private void Update()
    {
        PlayerUnit selectedPlayer = PlayerUnitCollection.Instance?.Current;

        if (selectedPlayer == this && Camera.main)
        {
            Camera.main.transform.SetPositionAndRotation(cameraPosition.position, cameraPosition.rotation);
        }
    }
}
