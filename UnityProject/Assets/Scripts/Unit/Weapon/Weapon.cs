using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Weapon : MonoBehaviour
{
    [SerializeField] private float rechargeTime = 1f;
    [SerializeField] private Bullet bullet = null;
    [SerializeField] private Transform firstBulletPos;
    [SerializeField] private Transform visualAttackRadius;

    private Coroutine attackCoroutine = null;
    private YieldInstruction waitFrame = new WaitForEndOfFrame();

    public void Attack(Unit target)
    {
        attackCoroutine = StartCoroutine(AttackTarget(target));
    }

    private IEnumerator AttackTarget(Unit target)
    {
        while (target)
        {
            if (!bullet)
            {
                yield return waitFrame;
                continue;
            }

            Vector3 dir = target.transform.position - transform.position;
            float dist2 = dir.sqrMagnitude;
            float attackRadius = bullet.MaxLength;

            if (dist2 > attackRadius * attackRadius)
            {
                yield return waitFrame;
                continue;
            }

            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            Physics.Raycast(ray, out hit);

            Unit other = hit.collider?.gameObject?.GetComponentInParent<Unit>();
            if (other != target)
            {
                yield return waitFrame;
                continue;
            }

            if (Shoot())
            {
                yield return new WaitForSeconds(rechargeTime);
                continue;
            }

            yield return waitFrame;
        }
    }

    private bool Shoot()
    {
        if (!bullet)
        {
            Debug.LogError("bullet == null!");
            return false;
        }

        if (!firstBulletPos)
        {
            Debug.LogError("firstBulletPos == null!");
            return false;
        }

        Bullet bulletObj = Instantiate(bullet, transform.position, transform.rotation);

        return true;
    }

    private void Update()
    {
        if (bullet && visualAttackRadius)
        {
            float scale = bullet.MaxLength * 2 * transform.lossyScale.GetMax();
            Vector3 lossyScale = visualAttackRadius.lossyScale;
            if (!Mathf.Approximately(lossyScale.x, scale))
            {
                visualAttackRadius.SetupLossyScale(new Vector3(scale, 0.1f, scale));
            }
        }
    }
}
