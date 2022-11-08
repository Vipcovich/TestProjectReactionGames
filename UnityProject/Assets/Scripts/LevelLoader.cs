using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelLoader : SingletonMonoBehaviourDontDestroy<LevelLoader>
{
    public int LevelIdx { get; private set; } = 0;

    private int menuLevelIdx = 0;
    private int firstLevelIdx = 1;

    public void LoadLevel(int levelIdx)
    {
        this.LevelIdx = levelIdx;
        GameManager.Instance?.ClearPause();
        SceneManager.LoadScene(levelIdx, LoadSceneMode.Single);
        GameManager.Instance?.SetState(GameManager.GameState.Gameplay);
    }

    public void LoadMenu()
    {
        GameManager.Instance?.ClearPause();
        SceneManager.LoadScene(menuLevelIdx, LoadSceneMode.Single);
        GameManager.Instance?.SetState(GameManager.GameState.Menu);
    }

    public void RepeatLevel()
    {
        LoadLevel(LevelIdx);
    }

    public void NewGame()
    {
        LoadLevel(firstLevelIdx);
    }

    public void NextLevel()
    {
        if (CanNextLevel())
        {
            LoadLevel(LevelIdx + 1);
        }
    }

    public bool CanNextLevel()
    {
        return (LevelIdx + 1) < SceneManager.sceneCountInBuildSettings;
    }
}
