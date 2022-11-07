using UnityEngine;
using System;

public class LevelRules : SingletonMonoBehaviour<LevelRules>
{
    public LevelStatistics Statistics { get; private set; }

    private int playerCreateCount = 0;
    private int playerInZoneCount = 0;

    protected override void Awake()
    {
        base.Awake();
        Statistics = GetComponent<LevelStatistics>()
            ?? GetComponentInChildren<LevelStatistics>()
            ?? gameObject.AddComponent<LevelStatistics>();
    }

    private void Start()
    {
        ExitZone.OnPlayerEnterToZone += OnPlayerEnterToZone;
        ExitZone.OnPlayerExitFromZone += OnPlayerExitFromZone;
        Unit.OnCreate += OnCreate;
        Unit.OnDead += OnDead;

        // save the number of PlayerUnits if they were created before LevelRules
        playerCreateCount = PlayerUnit.PlayerUnits.Count;
    }

    protected override void OnDestroy()
    {
        ExitZone.OnPlayerEnterToZone -= OnPlayerEnterToZone;
        ExitZone.OnPlayerExitFromZone -= OnPlayerExitFromZone;
        Unit.OnCreate -= OnCreate;
        Unit.OnDead -= OnDead;

        base.OnDestroy();
    }

    private void OnPlayerEnterToZone(PlayerUnit playerUnit)
    {
        playerInZoneCount++;
        if (playerInZoneCount >= playerCreateCount)
        {
            Win();
        }
    }

    private void OnPlayerExitFromZone(PlayerUnit playerUnit)
    {
        playerInZoneCount--;
    }

    private void OnCreate(Unit unit)
    {
        if (unit is PlayerUnit)
        {
            playerCreateCount++;
        }
    }

    private void OnDead(Unit unit)
    {
        if (unit is PlayerUnit)
        {
            Loose();
        }
    }

    private void Win()
    {
        if (Statistics)
        {
            Statistics.Data.statusWin = true;
        }

        GameManager.Instance?.SetState(GameManager.GameState.Result);
    }

    private void Loose()
    {
        if (Statistics)
        {
            Statistics.Data.statusWin = false;
        }
        GameManager.Instance?.SetState(GameManager.GameState.Result);
    }
}
