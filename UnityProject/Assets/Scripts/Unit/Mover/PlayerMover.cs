using UnityEngine;
using System.Collections.Generic;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotateSpeed = 10f;

    private Dictionary<MoveDir, int> moveDirsNow = new Dictionary<MoveDir, int>();
    private PlayerUnit owner;

    private Rigidbody rigidbody;

    private bool inGameplay => GameManager.Instance?.State == GameManager.GameState.Gameplay;

    private void Awake()
    {
        owner = GetComponentInParent<PlayerUnit>();
        rigidbody = owner?.GetComponentInChildren<Rigidbody>();
        OnSelectPlayerUnit(PlayerUnitCollection.Instance?.Current);

        PlayerUnitCollection.OnSelectPlayerUnit += OnSelectPlayerUnit;
    }

    private void OnDestroy()
    {
        PlayerUnitCollection.OnSelectPlayerUnit -= OnSelectPlayerUnit;
    }

    private void OnSelectPlayerUnit(PlayerUnit playerUnit)
    {
        enabled = (playerUnit != null && owner == playerUnit);
    }

    private void OnStartMove(MoveDir dir)
    {
        moveDirsNow.TryGetValue(dir, out int value);
        moveDirsNow[dir] = value + 1;
    }

    private void OnStopMove(MoveDir dir)
    {
        moveDirsNow.TryGetValue(dir, out int value);
        value--;
        if (value <= 0)
        {
            moveDirsNow.Remove(dir);
        }
    }

    private void FixedUpdate()
    {
        if (!enabled || !inGameplay)
        {
            return;
        }

        Vector3 dir = Vector3.zero;

        if (moveDirsNow.ContainsKey(MoveDir.Forward))
        {
            dir += transform.forward;
        }

        if (moveDirsNow.ContainsKey(MoveDir.Backward))
        {
            dir -= transform.forward;
        }

        if (dir != Vector3.zero)
        {
            if (rigidbody)
            {
                rigidbody.MovePosition(transform.position + dir * speed * Time.fixedDeltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.fixedDeltaTime);
            }
        }

        float angle = 0;

        if (moveDirsNow.ContainsKey(MoveDir.Left))
        {
            angle -= 1;
        }

        if (moveDirsNow.ContainsKey(MoveDir.Right))
        {
            angle += 1;
        }

        if (!Mathf.Approximately(angle, 0f))
        {
            if (rigidbody)
            {
                Quaternion deltaRotation = Quaternion.Euler(0, angle * rotateSpeed * Time.fixedDeltaTime, 0);
                rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
            }
            else
            {
                transform.Rotate(transform.up, angle * rotateSpeed * Time.fixedDeltaTime);
            }
        }
    }

    private void OnEnable()
    {
        ControlKeyboardButton.OnStartMove += OnStartMove;
        ControlKeyboardButton.OnStopMove += OnStopMove;

        ControlButton.OnStartMove += OnStartMove;
        ControlButton.OnStopMove += OnStopMove;

        Stop();
    }

    private void OnDisable()
    {
        ControlKeyboardButton.OnStartMove -= OnStartMove;
        ControlKeyboardButton.OnStopMove -= OnStopMove;

        ControlButton.OnStartMove -= OnStartMove;
        ControlButton.OnStopMove -= OnStopMove;

        Stop();
    }

    private void Stop()
    {
        moveDirsNow.Clear();
    }
}
