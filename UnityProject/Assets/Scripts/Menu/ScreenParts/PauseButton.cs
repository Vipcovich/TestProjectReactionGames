using UnityEngine;

public class PauseButton : ClickButton
{
    protected override void OnClick()
    {
        GameManager.Instance?.SetPause(true);
    }
}
