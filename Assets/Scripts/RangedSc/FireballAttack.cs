using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FireballAttack : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] public Transform AttackPoint;
    [SerializeField] private LayerMask EnemyMask;
    [SerializeField] private GameObject FireballPrefab;
    [SerializeField] private Transform RotationPoint;

    [Header("Attributes")]
    [SerializeField] private float attackRange;
    [SerializeField] private float LookRange;
    [SerializeField] private float attackCd = 2f;

    private float rotationSpeed = 500f;
    private Transform target;
    private bool canAttack = true;

    private void Update()
    {
        Collider2D[] RotateColliders = Physics2D.OverlapCircleAll(AttackPoint.position, LookRange, EnemyMask);


        if (RotateColliders.Length > 0)
        {

            float closestDistance = Mathf.Infinity;

            foreach (Collider2D col in RotateColliders)
            {
                float distance = Vector2.Distance(transform.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    target = col.transform;
                }
            }
            RotateTowardsTarget();
        }

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, EnemyMask);


        if (hitColliders.Length > 0)
        {

            float closestDistance = Mathf.Infinity;

            foreach (Collider2D col in hitColliders)
            {
                float distance = Vector2.Distance(transform.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    target = col.transform;
                }
            }
            if(canAttack)
            Attack();
        }
}
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(AttackPoint.position, LookRange);
    }
    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - RotationPoint.position.y, target.position.x - RotationPoint.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        RotationPoint.rotation = Quaternion.RotateTowards(RotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    private void Attack()
    {
        GameObject FireballObj = Instantiate(FireballPrefab, AttackPoint.position, Quaternion.identity);
        Fireball FireballScript = FireballObj.GetComponent<Fireball>();
        FireballScript.SetTarget(target);

        canAttack = false;
        Invoke("ResetAttack", attackCd);
    }
    private void ResetAttack()
    {
        canAttack = true;
    }

}
