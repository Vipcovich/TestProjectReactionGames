﻿using UnityEngine;
using System;

public partial class LevelStatistics : MonoBehaviour
{
    [Serializable]
    public class LevelStatisticsData
    {
        public bool statusWin = false;
        public long levelTimeTicks;
        public int enemyCreateCount = 0;
        public int enemyDeadCount = 0;

        public TimeSpan levelTime => new TimeSpan(levelTimeTicks);
    }

    public LevelStatisticsData Data { get; private set; } = new LevelStatisticsData();

    private void Awake()
    {
        GameManager.OnChangeGameState += OnChangeGameState;
        if (GameManager.Instance)
        {
            OnChangeGameState(GameManager.Instance.State);
        }
    }

    private void OnChangeGameState(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.Gameplay)
        {
            Subscribe();
        }
        else 
        {
            Unsubscribe();
        }
    }

    private void Subscribe()
    {
        Unit.OnCreate += OnCreate;
        Unit.OnDead += OnDead;
    }

    private void Unsubscribe()
    {
        Unit.OnCreate -= OnCreate;
        Unit.OnDead -= OnDead;
    }

    private void OnDestroy()
    {
        GameManager.OnChangeGameState -= OnChangeGameState;
        Unsubscribe();
    }

    private void OnCreate(Unit unit)
    {
        if (!(unit is PlayerUnit))
        {
            Data.enemyCreateCount++;
        }
    }

    private void OnDead(Unit unit)
    {
        if (!(unit is PlayerUnit))
        {
            Data.enemyCreateCount++;
        }
    }

    private void Update()
    {
        if (GameManager.Instance && GameManager.Instance.State == GameManager.GameState.Gameplay)
        {
            Data.levelTimeTicks += TimeSpan.FromSeconds(Time.deltaTime).Ticks;
        }
    }

    public string GetLevelTimeStr()
    {
        return string.Format("{0:D2}:{1:D2}", (int)Data.levelTime.TotalMinutes, Data.levelTime.Seconds);
    }

    public string GetCreateKillEnemiesStr()
    {
        return string.Format("{0}/{1}", Data.enemyCreateCount, Data.enemyDeadCount);
    }
}
