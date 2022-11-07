using UnityEngine;

public class PauseButton : ClickButton
{
    protected override void OnClick()
    {
        Time.timeScale = 0f;
    }
}
