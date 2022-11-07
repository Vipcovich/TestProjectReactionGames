public class RepeatButton : ClickButton
{
    private void Start()
    {
        bool win = LevelRules.Instance?.Statistics?.Data?.statusWin ?? false;
        gameObject.SetActive(!win);
    }

    protected override void OnClick()
    {
        LevelLoader.Instance?.RepeatLevel();
    }
}
