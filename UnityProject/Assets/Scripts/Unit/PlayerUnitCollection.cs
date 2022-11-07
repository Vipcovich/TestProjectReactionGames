using UnityEngine;
using System;
using System.Collections.Generic;

public partial class PlayerUnitCollection : SingletonMonoBehaviour<PlayerUnitCollection>
{
    public static event Action<PlayerUnit> OnSelectPlayerUnit;

    private List<PlayerUnit> playerUnits => PlayerUnit.PlayerUnits;

    public PlayerUnit Current { get; private set; }

    private void Start()
    {
        PlayerUnit.OnCreatePlayerUnit += OnCreatePlayerUnit;
        PlayerUnit.OnDestroyPlayerUnit += OnDestroyPlayerUnit;

        ControlKeyboardButton.OnChangePlayer += SelectNextPlayerUnit;
        NextPlayerUnitButton.OnChangePlayer += SelectNextPlayerUnit;

        SelectNextPlayerUnit();
    }

    protected override void OnDestroy()
    {
        PlayerUnit.OnCreatePlayerUnit -= OnCreatePlayerUnit;
        PlayerUnit.OnDestroyPlayerUnit -= OnDestroyPlayerUnit;

        ControlKeyboardButton.OnChangePlayer -= SelectNextPlayerUnit;
        NextPlayerUnitButton.OnChangePlayer -= SelectNextPlayerUnit;

        base.OnDestroy();
    }

    private void OnCreatePlayerUnit(PlayerUnit playerUnit)
    {
        if (Current == null)
        {
            SelectCurrentPlayer(playerUnit);
        }
    }

    private void OnDestroyPlayerUnit(PlayerUnit playerUnit)
    {
        if (Current == null)
        {
            SelectNextPlayerUnit();
        }
    }

    private void SelectCurrentPlayer(PlayerUnit playerUnit)
    {
        Current = playerUnit;
        OnSelectPlayerUnit.SafetyInvoke(Current);
    }

    public void SelectNextPlayerUnit()
    {
        PlayerUnit nextPlayerUnit = GetNextPlayerUnit();
        SelectCurrentPlayer(nextPlayerUnit);
    }

    private PlayerUnit GetNextPlayerUnit()
    {
        if (playerUnits.Count == 0)
        {
            return null;
        }

        if (Current == null)
        {
            return playerUnits[0];
        }

        int idx = playerUnits.FindIndex(obj => obj == Current);
        idx++;

        return (idx < playerUnits.Count) ? playerUnits[idx] : playerUnits[0];
    }
}
