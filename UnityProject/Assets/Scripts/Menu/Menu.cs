using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameState = GameManager.GameState;

public class Menu : SingletonMonoBehaviourDontDestroy<Menu>
{
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject gameplayScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject resultScreen;

    private GameObject currentScreen;

    private void OnEnable()
    {
        GameManager.OnChangeGameState += OnChangeGameState;

        if (GameManager.Instance)
        {
            OnChangeGameState(GameManager.Instance.State);
        }
    }

    private void OnDisable()
    {
        GameManager.OnChangeGameState -= OnChangeGameState;
    }

    private void ShowScreen(GameObject screen)
    {
        if (currentScreen)
        {
            Destroy(currentScreen);
            currentScreen = null;
        }

        if (!screen)
        {
            Debug.LogError("Can't show screen! screen == null");
            return;
        }

        currentScreen = Instantiate(screen, transform);
    }

    private void OnChangeGameState(GameState state)
    {
        switch(state)
        {
            case GameState.Init:
            case GameState.Menu:
                ShowScreen(startScreen);
                break;

            case GameState.Gameplay:
                ShowScreen(gameplayScreen);
                break;

            case GameState.Pause:
                ShowScreen(pauseScreen);
                break;

            case GameState.Result:
                ShowScreen(resultScreen);
                break;

            default:
                break;
        }
    }
}
