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
        SceneManager.LoadScene(levelIdx, LoadSceneMode.Single);
        GameManager.Instance?.SetState(GameManager.GameState.Gameplay);
        Time.timeScale = 1f;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(menuLevelIdx, LoadSceneMode.Single);
        GameManager.Instance?.SetState(GameManager.GameState.Menu);
        Time.timeScale = 1f;
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
