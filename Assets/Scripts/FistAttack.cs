using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;

public class FistAttack : MonoBehaviour
{
    public static FistAttack main;
    [Header("Refs")]
    [SerializeField] public Transform AttackPoint;
    [SerializeField] private LayerMask EnemyMask;
    [SerializeField] private Transform InitialPosition;
    [SerializeField] public Transform normalPosition;

    [Header("Attributes")]
    [SerializeField] private float attackRange;
    [SerializeField] private float dmg;
    [SerializeField] public float moveSpeed = 5.0f;

    private bool isAttacking;
    private bool isHit;
    private Transform target;
    private bool canAttack = true;
    private void Awake()
    {
        main = this;
    }
    private void Update()
    {
        
        
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
            
            if (canAttack)
            {
                Attack();
            }
            else return;
        }
        else if (isAttacking == true)
            {
            CheckIsBack();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }

    
    private void Attack()
    {
      
        Vector2 direction = (target.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        isAttacking = true;
    }

    private void BackToPos()
    {
        transform.DOLocalMove(normalPosition.localPosition, 1f).SetEase(Ease.OutCubic).OnComplete(ResetAttack);
        isAttacking = false;
    }

    private void ResetAttack()
    {
        isHit = false;
        canAttack = true;
    }
    private void OnCollisionEnter2D(Collision2D collision2D)
    {

        collision2D.gameObject.GetComponent<Enemy>().takeDamage(dmg);
        isHit = true;
        canAttack = false;
        
        BackToPos();

    }

    private void CheckIsBack()
    {
        if (!isHit)
        {
            BackToPos();
        }
    }
}
