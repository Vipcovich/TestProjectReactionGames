using UnityEngine;
using System;

public class NextPlayerUnitButton : ClickButton
{
    public static event Action OnChangePlayer;

    protected override void OnClick()
    {
        OnChangePlayer.SafetyInvoke();
    }
}
