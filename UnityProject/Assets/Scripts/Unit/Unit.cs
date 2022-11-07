using System;
using UnityEngine;

public partial class Unit : MonoBehaviour
{
    public static event Action<Unit> OnCreate;
    public static event Action<Unit> OnDead;

    [SerializeField] private float health = 10f;
    public float Health => health;

    [SerializeField] private float maxHealth = 10f;
    public float MaxHealth => maxHealth;

    protected virtual void Start()
    {
        OnCreate.SafetyInvoke(this);
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Max(0, health - damage);
        if (health <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        OnDead.SafetyInvoke(this);
        Destroy(gameObject);
    }
}

