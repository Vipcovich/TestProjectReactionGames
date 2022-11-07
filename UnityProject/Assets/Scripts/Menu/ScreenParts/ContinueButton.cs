using UnityEngine;

public class ContinueButton : ClickButton
{
    protected override void OnClick()
    {
        Time.timeScale = 1f;
    }
}
