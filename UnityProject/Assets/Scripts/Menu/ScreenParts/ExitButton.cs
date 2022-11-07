public class ExitButton : ClickButton
{
    protected override void OnClick()
    {
        LevelLoader.Instance?.LoadMenu();
    }
}
