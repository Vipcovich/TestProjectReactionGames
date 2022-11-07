using UnityEngine;

public partial class Bullet : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float maxLength = 100f;
    [SerializeField] private float speed = 50f;

    public float MaxLength => maxLength;

    private Vector3 startPos;
    private float maxLength2;

    private void Awake()
    {
        startPos = transform.position;
        maxLength2 = maxLength * maxLength;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, speed * Time.deltaTime);
        if ((transform.position - startPos).sqrMagnitude >= maxLength2)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision?.gameObject?.GetComponentInParent<Unit>()?.TakeDamage(damage);
        Destroy(gameObject);
    }
}
