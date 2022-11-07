using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIUnit : Unit
{
    [SerializeField] private PathMover mover;
    [SerializeField] private Weapon weapon;
    [SerializeField] private Detector detector;
    [SerializeField] private float collisionDamage = 1f;

    protected override void Start()
    {
        base.Start();
        if (detector)
        {
            detector.OnChangeTarget += OnTarget;
        }
    }

    private void OnTarget(PlayerUnit target)
    {
        mover?.SetTarget(target?.gameObject);
        weapon?.Attack(target);
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerUnit playerUnit = collision?.gameObject?.GetComponentInParent<PlayerUnit>();
        if (playerUnit)
        {
            playerUnit?.TakeDamage(collisionDamage);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (detector)
        {
            detector.OnChangeTarget -= OnTarget;
        }
    }
}
