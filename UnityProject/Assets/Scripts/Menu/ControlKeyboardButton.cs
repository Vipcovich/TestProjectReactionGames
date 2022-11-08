using System;
using System.Collections.Generic;
using UnityEngine;

public class ControlKeyboardButton : MonoBehaviour
{
    public static event Action<MoveDir> OnStartMove;
    public static event Action<MoveDir> OnStopMove;

    public static event Action OnChangePlayer;

    [Serializable]
    public class MoveControlKey
    {
        public KeyCode KeyCode = KeyCode.None;
        public MoveDir MoveDir = MoveDir.None;
    }

    [SerializeField] private List<MoveControlKey> moveControlKeys = new List<MoveControlKey>()
    {
        { new MoveControlKey { KeyCode = KeyCode.W, MoveDir = MoveDir.Forward }},
        { new MoveControlKey { KeyCode = KeyCode.S, MoveDir = MoveDir.Backward }},
        { new MoveControlKey { KeyCode = KeyCode.A, MoveDir = MoveDir.Left }},
        { new MoveControlKey { KeyCode = KeyCode.D, MoveDir = MoveDir.Right }},

        { new MoveControlKey { KeyCode = KeyCode.UpArrow,    MoveDir = MoveDir.Forward }},
        { new MoveControlKey { KeyCode = KeyCode.DownArrow,  MoveDir = MoveDir.Backward }},
        { new MoveControlKey { KeyCode = KeyCode.LeftArrow,  MoveDir = MoveDir.Left }},
        { new MoveControlKey { KeyCode = KeyCode.RightArrow, MoveDir = MoveDir.Right }},
    };

    [SerializeField] private KeyCode nextPlayerKeyCode = KeyCode.Tab;
    [SerializeField] private KeyCode pauseKeyCode = KeyCode.Escape;

    private HashSet<KeyCode> sendPress = new HashSet<KeyCode>();

    private void Update()
    {
        for (int i = 0; i < moveControlKeys.Count; i++)
        {
            MoveControlKey control = moveControlKeys[i];

            KeyCode key = control.KeyCode;
            bool pressed = Input.GetKey(key);
            bool sent = sendPress.Contains(key);

            if (pressed && !sent)
            {
                sendPress.Add(key);
                OnStartMove.SafetyInvoke(control.MoveDir);
            }
            else if (!pressed && sent)
            {
                sendPress.Remove(key);
                OnStopMove.SafetyInvoke(control.MoveDir);
            }
        }

        if (Input.GetKeyDown(nextPlayerKeyCode))
        {
            OnChangePlayer.SafetyInvoke();
        }

        if (Input.GetKeyDown(pauseKeyCode))
        {
            GameManager.Instance?.SetPause(GameManager.Instance.State != GameManager.GameState.Pause);
        }
    }
}
