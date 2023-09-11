using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistAttack : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Transform AttackPoint;
    [SerializeField] private LayerMask EnemyMask;

    [Header("Attributes")]
    [SerializeField] private float attackRange;
    [SerializeField] private float dmg;

    private void Update()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange, EnemyMask);

        foreach (Collider2D col in hitColliders)
        {
            Debug.Log("Target within range");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    private void Attack()
    {

        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, EnemyMask);

        foreach (Collider2D enemy in hitEnemy)
        {
            if (enemy.TryGetComponent<Enemy>(out var e))
            {
                e.takeDamage(dmg);
            }
        }

    }
}
