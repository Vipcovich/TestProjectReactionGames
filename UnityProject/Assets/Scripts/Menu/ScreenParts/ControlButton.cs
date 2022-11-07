using UnityEngine;
using System;

public class ControlButton : ClickButton
{
    public static event Action<MoveDir> OnStartMove;
    public static event Action<MoveDir> OnStopMove;

    [SerializeField] private MoveDir moveDir = MoveDir.None;

    protected override void OnDown() 
    {
        OnStartMove.SafetyInvoke(moveDir);
    }

    protected override void OnUp() 
    {
        OnStopMove.SafetyInvoke(moveDir);
    }
}
