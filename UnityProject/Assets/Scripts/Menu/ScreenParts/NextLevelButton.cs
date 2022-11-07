public class NextLevelButton : ClickButton
{
    private void Start()
    {
        bool win = LevelRules.Instance?.Statistics?.Data?.statusWin ?? false;
        bool canNextLevel = LevelLoader.Instance?.CanNextLevel() ?? false;
        gameObject.SetActive(win && canNextLevel);
    }

    protected override void OnClick()
    {
        LevelLoader.Instance?.NextLevel();
    }
}
