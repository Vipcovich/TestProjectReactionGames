using UnityEngine;

public class ContinueButton : ClickButton
{
    protected override void OnClick()
    {
        GameManager.Instance?.SetPause(false);
    }
}
