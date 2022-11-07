public class NewGameButton : ClickButton
{
    protected override void OnClick()
    {
        LevelLoader.Instance?.NewGame();
    }
}
