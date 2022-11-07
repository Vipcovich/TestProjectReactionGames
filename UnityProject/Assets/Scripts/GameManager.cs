using System;
using UnityEngine;

public class GameManager : SingletonMonoBehaviourDontDestroy<GameManager>
{
    public enum GameState
    {
        Init,
        Menu,
        Gameplay,
        Result,
        Pause,
    }

    [SerializeField] private GameState state = GameState.Init;

    public GameState State => state;
    public GameState PrevState { get; private set; } = GameState.Init;

    public static event Action<GameState> OnChangeGameState;

    private void Start()
    {
        OnChangeGameState.SafetyInvoke(State);
    }

    public void SetState(GameState newState)
    {
        if (newState == state)
        {
            return;
        }

        PrevState = state;
        state = newState;

        OnChangeGameState.SafetyInvoke(state);
    }

    private void Update()
    {
        bool isPause = Mathf.Approximately(Time.timeScale, 0f);
        if (isPause && state == GameState.Gameplay)
        {
            SetState(GameState.Pause);
        }
        else if (!isPause && state == GameState.Pause)
        {
            SetState(PrevState);
        }
    }

    private void OnValidate()
    {
        OnChangeGameState.SafetyInvoke(state);
    }
}
